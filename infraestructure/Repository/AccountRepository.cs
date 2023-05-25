using Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace infraestructure.Repository
{
    public class AccountRepository
    {
        private string _connectionString;
        private Npgsql.NpgsqlConnection connection;
        public AccountRepository(string connectionString)
        {
            this._connectionString = connectionString;
            this.connection = new Npgsql.NpgsqlConnection(this._connectionString);
        }

        public string insert(AccountModel account)
        {
            try
            {
                String query = "insert into account(id_account, name, number, balance, limit_balance, limit_transfer, status) " +
                     " values(@id_account, @name, @number, @balance, @limit_balance, @limit_transfer, @status)";
                connection.Open();
                connection.Execute(query, account);
                connection.Close();

                return "Se inserto correctamente...";
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public string modify(AccountModel account, int id)
        {
            try
            {
                connection.Execute($"UPDATE account SET " +
                    "id_account = @id_account, " +
                    "name = @name, " +
                    "number = @number, " +
                    "balance = @balance, " +
                    "limit_balance = @limit_balance, " +
                    "limit_transfer = @limit_transfer, " +
                    "status = @status " +

                    $"WHERE id = {id}", account);
                return "Se modificaron los datos correctamente...";
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public string delete(int id)
        {
            try
            {
                connection.Execute($" DELETE FROM account WHERE id = {id}");
                return "Se eliminó correctamente el registro...";
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public AccountModel getById(int id)
        {
            try
            {
                return connection.QueryFirst<AccountModel>($"SELECT * FROM account WHERE id = {id}");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public IEnumerable<AccountModel> read()
        {
            try
            {
                // get all accounts
                return connection.Query<AccountModel>("SELECT * FROM account");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}