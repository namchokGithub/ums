using UMS.Models;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;

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
        Account GetByID(string id);
        SqlParameter GetStatus(string username);
        void ToggleStatus(string id);
        void UpdateName(Account _account);
        void UpdateRole(Account _account);
        void UpdateNameAndPassword(Account _account);
    } // End Interface IAccount
}
