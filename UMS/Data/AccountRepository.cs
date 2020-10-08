using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using UMS.Models;

/*
 * Name: AccountRepository
 * Author: Namchok Singhachai
 * Description: Class of account to connect data layer.
 */

namespace UMS.Data
{
    public class AccountRepository : Repository<Account>, IAccountRepository
    {
        protected readonly AuthDbContext _context;
        /*
         * Name: AccountRepository
         * Parametor: context(AuthDbContext)
         * Description: The constructor for set context
         */
        public AccountRepository(AuthDbContext context) : base(context)
        {
            _context = context;
        } // End constructor

        /*
         * Name: FindByUsername
         * Parametor: username(string), status(string)
         * Description: The search for user by username and status.
         */
        public SqlParameter FindByUsername(string username, string status)
        {
            var checkExits = new SqlParameter("@returnVal", SqlDbType.Int) { Direction = ParameterDirection.Output }; // Set parameter for get value
            var sqlText = $"EXEC @returnVal=[dbo].ums_Check_user '{username}', '{status}'";// Return value from sture procudure
            _context.Account.FromSqlRaw(sqlText, checkExits);
            var result = false;
            while (!result)
            {
                try
                {
                    _context.SaveChanges();
                    result = true; // If successfully
                }
                catch (Exception e)
                {
                    throw e;
                } // End try catch
            } // Checking successfully
            return checkExits;
        } // End FindByUsername

        public List<Account> GetAll()
        {
            throw new System.NotImplementedException();
        }

        public Account GetByID()
        {
            throw new System.NotImplementedException();
        }

        public SqlParameter GetStatus(string username, string status)
        {
            throw new System.NotImplementedException();
        }

        public void ToggleStatus(string id)
        {
            throw new System.NotImplementedException();
        }

        public void UpdateName(Account _account)
        {
            throw new System.NotImplementedException();
        }

        public void UpdateRole(Account _account)
        {
            throw new System.NotImplementedException();
        }
    } // End AccountRepository
}
