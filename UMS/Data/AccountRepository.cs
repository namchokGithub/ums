using System.Collections.Generic;
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

        public SqlParameter FindByUsername(string username, string status)
        {
            throw new System.NotImplementedException();
        }

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
