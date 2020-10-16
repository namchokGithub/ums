using System;
using System.Linq;
using System.Data;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using User_Management_System.Models;

/*
 * Name: AccountRepository
 * Author: Namchok Singhachai
 * Description: Class of account to connect data layer.
 */

namespace User_Management_System.Data
{
    public class AccountRepository : Repository<Management>, IAccountRepository
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
         * Author: Namchok Singhachai
         * Description: The search for user by username and status (In controller is Function CheckIsExist).
         */
        public SqlParameter FindByUsername(string username, string status)
        {
            var checkExits = new SqlParameter("@returnVal", SqlDbType.Int)
            {
                Direction = ParameterDirection.Output
            }; // Set parameter for get value
            _context.Database.ExecuteSqlRaw($"EXEC @returnVal=[dbo].ums_Check_user '{username}', '{status}'", checkExits);
            _context.SaveChanges();
            _context.Dispose();
            return checkExits;
        } // End FindByUsername

        /*
         * Name: FindByUsernameAsync
         * Parametor: username(string), status(string)
         * Author: Namchok Singhachai
         * Description: The search for user by username and status (In controller is Function CheckIsExist).
         */
        public async Task<SqlParameter> FindByUsernameAsync(string username, string status)
        {
            var checkExits = new SqlParameter("@returnVal", SqlDbType.Int)
            {
                Direction = ParameterDirection.Output
            }; // Set parameter for get value
            await _context.Database.ExecuteSqlRawAsync($"EXEC @returnVal=[dbo].ums_Check_user '{username}', '{status}'", checkExits);
            await _context.SaveChangesAsync();
            await _context.DisposeAsync();
            return checkExits;
        } // End FindByUsernameAsync

        /*
         * Name: GetAll
         * Author: Namchok Singhachai
         * Description: The getting list of all users.
         */
        public List<Management> GetAll()
        {
            return _context.Management.FromSqlRaw("EXEC [dbo].ums_Get_all_active_user").ToList<Management>();
        } // End get all

        /*
         * Name: GetAllAsync
         * Author: Namchok Singhachai
         * Description: The getting list of all users.
         */
        public async Task<List<Management>> GetAllAsync()
        {
            return await _context.Management.FromSqlRaw("EXEC [dbo].ums_Get_all_active_user").ToListAsync<Management>();
        } // End GetAllAsync

        /*
         * Name: GetByID
         * Parameter: id(string)
         * Author: Namchok Singhachai
         * Description: The getting a user is already active on the system by id.
         */
        public Management GetByID(string id)
        {
            return _context.Management.FromSqlRaw($"EXEC [dbo].ums_Get_user_by_Id '{id}'").AsEnumerable<Management>().FirstOrDefault();
        } // End GetByID

        /*
         * Name: GetByIDAsync
         * Parameter: id(string)
         * Author: Namchok Singhachai
         * Description: The getting a user is already active on the system by id.
         */
        public async Task<Management> GetByIDAsync(string id)
        {
            var result = await _context.Management.FromSqlRaw($"EXEC [dbo].ums_Get_user_by_Id '{id}'").ToListAsync();
            return result.FirstOrDefault<Management>();
        } // End GetByIDAsync

        /*
         * Name: GetStatus
         * Parameter: username(string), status(string)
         * Description: The checking user is already exist on system by username (GetStatusUser).
         */
        public SqlParameter GetStatus(string username)
        {
            var status = new SqlParameter("@paramout_status", SqlDbType.Int)
            {
                IsNullable = true,
                Direction = ParameterDirection.Output,
                Size = 10,
                Value = DBNull.Value
            }; // Set parameter for get value
            _context.Database.ExecuteSqlRaw($@"EXEC @paramout_status=[dbo].ums_Get_status_user '{username}'", status);
            _context.SaveChanges();
            _context.Dispose();
            return status;
        } // End GetStatus

        /*
         * Name: GetStatusAsync
         * Parameter: username(string), status(string)
         * Description: The checking user is already exist on system by username (GetStatusUser).
         */
        public async Task<SqlParameter> GetStatusAsync(string username)
        {
            var status = new SqlParameter("@paramout_status", SqlDbType.Int)
            {
                IsNullable = true,
                Direction = ParameterDirection.Output,
                Size = 10,
                Value = DBNull.Value
            }; // Set parameter for get value
            await _context.Database.ExecuteSqlRawAsync($@"EXEC @paramout_status=[dbo].ums_Get_status_user '{username}'", status);
            await _context.SaveChangesAsync();
            await _context.DisposeAsync();
            return status;
        } // End GetStatusAsync

        /*
         * Name: ToggleStatus
         * Parameter: id(string)
         * Description: Account deactivation.
         */
        public void ToggleStatus(string id)
        {
            _context.Database.ExecuteSqlRaw($"ums_Delete_user '{id}'");
        } // End ToggleStatus

        /*
         * Name: ToggleStatusAsync
         * Parameter: id(string)
         * Description: Account deactivation.
         */
        public async Task ToggleStatusAsync(string id)
        {
            await _context.Database.ExecuteSqlRawAsync($"ums_Delete_user '{id}'");
        } // End ToggleStatusAsync

        /*
         * Name: UpdateName
         * Parameter: _account(Account)
         * Description: The user profile editing (Name).
         */
        public void UpdateName(Management _account)
        {
            _context.Database.ExecuteSqlRaw($"ums_Update_name_user '{_account.acc_Id}', '{_account.acc_Firstname}', '{_account.acc_Lastname}'");
        } // End UpdateName

        /*
         * Name: UpdateNameAsync
         * Parameter: _account(Account)
         * Description: The user profile editing (Name).
         */
        public async Task UpdateNameAsync(Management _account)
        {
            await _context.Database.ExecuteSqlRawAsync($"ums_Update_name_user '{ _account.acc_Id}', '{ _account.acc_Firstname}', '{ _account.acc_Lastname}'");
        } // End UpdateNameAsync

        /*
         * Name: UpdateNameAndPassword
         * Parameter: _account(Account)
         * Description: The user profile editing (Name and Password).
         */
        public void UpdateNameAndPassword(Management _account)
        {
            _context.Database.ExecuteSqlRaw($"ums_Update_all '{ _account.acc_Id}', '{ _account.acc_Firstname}', '{ _account.acc_Lastname}', '{ _account.acc_PasswordHash}'");
        } // End UpdateNameAndPassword

        /*
         * Name: UpdateNameAndPasswordAsync
         * Parameter: _account(Account)
         * Description: The user profile editing (Name and Password).
         */
        public async Task UpdateNameAndPasswordAsync(Management _account)
        {
            await _context.Database.ExecuteSqlRawAsync($"ums_Update_all '{ _account.acc_Id}', '{ _account.acc_Firstname}', '{ _account.acc_Lastname}', '{ _account.acc_PasswordHash}'");
        } // End UpdateNameAndPasswordAsync

        /*
         * Name: UpdateRole
         * Parameter: _account(Account)
         * Description: The user profile editing (Role).
         */
        public void UpdateRole(Management _account)
        {
            _context.Database.ExecuteSqlRaw($"ums_Update_role_user '{_account.acc_Id}', '{_account.acc_Rolename}'");
        } // End UpdateRole

        /*
         * Name: UpdateRoleAsync
         * Parameter: _account(Account)
         * Description: The user profile editing (Role).
         */
        public async Task UpdateRoleAsync(Management _account)
        {
            await _context.Database.ExecuteSqlRawAsync($"ums_Update_role_user '{_account.acc_Id}', '{_account.acc_Rolename}'");
        } // End UpdateRoleAsync
    } // End AccountRepository
}
