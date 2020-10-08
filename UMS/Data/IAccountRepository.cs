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
        SqlParameter FindByUsername(string username, string status);
        List<Account> GetAll();
        Account GetByID();
        SqlParameter GetStatus(string username, string status);
        void ToggleStatus(string id);
        void UpdateName(Account _account);
        void UpdateRole(Account _account);
    } // End Interface IAccount
}
