using infraestructure.Repository;
using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services
{
    public class OperationService
    {

        private OperationService operationService;

        private AccountRepository accountRepository;

        public OperationService(AccountRepository accountRepository)
        {
            this.accountRepository = accountRepository;
        }

        public string Transfer(int sourceAccountId, int targetAccountId, double amount)
        {
            var sourceAccount = accountRepository.getById(sourceAccountId);
            var targetAccount = accountRepository.getById(targetAccountId);

            if (sourceAccount.id == targetAccount.id)
            {
                throw new ArgumentException("No se puede transferir en la misma cuenta");
            }

            if (sourceAccount.status == false || targetAccount.status == false)
            {
                throw new ArgumentException("Cuenta invalida o inhabilitada");
            }

            if (sourceAccount.limit_transfer < amount || targetAccount.limit_balance < (targetAccount.balance + amount))
            {
                throw new ArgumentException("Desbordamiento de limite de transferencia o limite de saldo");
            }

            if (sourceAccount.balance < amount)
            {
                throw new InvalidOperationException("Saldo insuficiente");
            }

            sourceAccount.balance -= amount;
            targetAccount.balance += amount;

            accountRepository.modify(sourceAccount, sourceAccountId);
            accountRepository.modify(targetAccount, targetAccountId);
            return "Transferencia exitosa!";
        }

        public string Deposit(double amount, int accountId)
        {
            var account = accountRepository.getById(accountId);
            if (account.status == false)
            {
                throw new ArgumentException("Cuenta invalida o inhabilitada");
            }
            if (account.limit_balance < (account.balance + amount))
            {
                throw new ArgumentException("Limite de saldo superado");
            }
            account.balance += amount;

            accountRepository.modify(account, accountId);

            return "Deposito exitoso!";
        }

        public string Devolution(double amount, int accountId)
        {
            var account = accountRepository.getById(accountId);
            if (account.status == false)
            {
                throw new ArgumentException("Cuenta invalida o inhabilitada");
            }
            if (account.balance < amount)
            {
                throw new InvalidOperationException("Saldo insuficiente");
            }
            account.balance -= amount;
            accountRepository.modify(account, accountId);
            return "Devolucion exitosa!";
        }

        public string Extract(double amount, int accountId)
        {
            var account = accountRepository.getById(accountId);
            if (account.status == false)
            {
                throw new ArgumentException("Cuenta invalida o inhabilitada");
            }
            if (account.balance < amount)
                {
                throw new InvalidOperationException("Saldo insuficiente");
            }
            account.balance -= amount;
            accountRepository.modify(account, accountId);

            var updatedAccount = accountRepository.getById(accountId);

            return "Extraccion exitosa! Su saldo restante es: " + updatedAccount.balance;
            
        }

        public string Block(int accountId)
        {
            var account = accountRepository.getById(accountId);
            if (account == null)
            {
                throw new ArgumentException("Invalid account ID");
            }

            account.status = false;
            
            accountRepository.modify(account, accountId);

            var updatedAccount = accountRepository.getById(accountId);

            return "Cuenta bloqueada exitosamente";

        }
    }
}
