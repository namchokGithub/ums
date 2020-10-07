using System.Collections.Generic;
using System.Data.SqlClient;
using UMS.Models;

/*
 * Name: IAccountRepository
 * Author: Namchok Singchai
 * Description: Interface for repository pattern.
 */

namespace UMS.Data
{
    public interface IAccountRepository : IRepository<Account>
    {
        List<Account> GetAll();
        Account GetByID();
        void UpdateName(Account _account);
        void UpdateRole(Account _account);
        void ToggleStatus(string id);
        SqlParameter FindByUsername(string username, string status);
        SqlParameter GetStatus(string username, string status);
    } // End Interface IAccount
}
