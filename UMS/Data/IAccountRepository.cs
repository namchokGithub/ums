using UMS.Models;
using System.Threading.Tasks;
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
        Task<SqlParameter> FindByUsernameAsync(string username, string status);
        List<Account> GetAll();
        Task<List<Account>> GetAllAsync();
        Account GetByID(string id);
        Task<Account> GetByIDAsync(string id);
        SqlParameter GetStatus(string username);
        Task<SqlParameter> GetStatusAsync(string username);
        void ToggleStatus(string id);
        Task ToggleStatusAsync(string id);
        void UpdateName(Account _account);
        Task UpdateNameAsync(Account _account);
        void UpdateRole(Account _account);
        Task UpdateRoleAsync(Account _account);
        void UpdateNameAndPassword(Account _account);
        Task UpdateNameAndPasswordAsync(Account _account);
    } // End Interface IAccount
}
