using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using UMS.Areas.Identity.Data;
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
        Account GetByID(string id);
        SqlParameter GetStatus(string username);
        void ToggleStatus(string id);
        void UpdateName(Account _account);
        void UpdateRole(Account _account);
    } // End Interface IAccount
}
