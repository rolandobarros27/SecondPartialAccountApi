using infraestructure.Repository;
using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services
{
    public class AccountService
    {
        private AccountRepository accountRepository;

        public AccountService(string connectionString)
        {
            this.accountRepository = new AccountRepository(connectionString);
        }

        public string insert(AccountModel account)
        {
            return validateAccount(account) ? accountRepository.insert(account) : throw new Exception("Error en la validacion");
        }

        public string modify(AccountModel account, int id)
        {
            if (accountRepository.getById(id) != null)
            {
                return validateAccount(account) ? accountRepository.modify(account, id) : throw new Exception("Error en la validacion");
            }
            else
            {
                throw new Exception("No se encontro el registro");
            }
        }

        public string delete(int id)
        {
            return accountRepository.delete(id);
        }

        public AccountModel getByid(int id)
        {
            return accountRepository.getById(id);
        }

        public IEnumerable<AccountModel> read()
        {
            return accountRepository.read();
        }

        private bool validateAccount(AccountModel account)
        {
            /*if (account.Name.Trim().Length > 2)
            {
                return false;
            }*/

            return true;
        }
    }
}