using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using User_Management_System.Models;

/*
 * Name: IAccountRepository
 * Author: Namchok Singchai
 * Description: Interface for repository pattern.
 */

namespace User_Management_System.Data
{
    public interface IAccountRepository : IRepository<Management>
    {
        SqlParameter FindByUsername(string username, string status);
        Task<SqlParameter> FindByUsernameAsync(string username, string status);
        List<Management> GetAll();
        Task<List<Management>> GetAllAsync();
        Management GetByID(string id);
        Task<Management> GetByIDAsync(string id);
        SqlParameter GetStatus(string username);
        Task<SqlParameter> GetStatusAsync(string username);
        void ToggleStatus(string id);
        Task ToggleStatusAsync(string id);
        void UpdateName(Management _account);
        Task UpdateNameAsync(Management _account);
        void UpdateRole(Management _account);
        Task UpdateRoleAsync(Management _account);
        void UpdateNameAndPassword(Management _account);
        Task UpdateNameAndPasswordAsync(Management _account);
    } // End Interface IAccount
}