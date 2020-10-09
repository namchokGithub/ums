using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Linq;
using UMS.Areas.Identity.Data;
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
         * Author: Namchok Singhachai
         * Description: The search for user by username and status (In controller is Function CheckIsExist).
         */
        public SqlParameter FindByUsername(string username, string status)
        {
            try
            {
                var checkExits = new SqlParameter("@returnVal", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Output
                }; // Set parameter for get value
                _context.Database.ExecuteSqlRaw($"EXEC @returnVal=[dbo].ums_Check_user '{username}', '{status}'", checkExits);
                _context.SaveChanges();
                _context.Dispose();
                return checkExits;
            } catch (Exception e)
            {
                throw e;
            } // End try catch
        } // End FindByUsername

        /*
         * Name: GetAll
         * Author: Namchok Singhachai
         * Description: The getting list of all users.
         */
        public List<Account> GetAll()
        {
            return _context.Account.FromSqlRaw("EXEC [dbo].ums_Get_all_active_user").ToList<Account>();
        } // End get all

        /*
         * Name: GetByID
         * Parameter: id(string)
         * Author: Namchok Singhachai
         * Description: The getting a user is already active on the system by id.
         */
        public Account GetByID(string id)
        {
            return _context.Account.FromSqlRaw($"EXEC [dbo].ums_Get_user_by_Id '{id}'").AsEnumerable<Account>().FirstOrDefault();
        } // End GetByID

        /*
         * Name: GetStatus
         * Parameter: username(string), status(string)
         * Description: The checking user is already exist on system by username (GetStatusUser).
         */
        public SqlParameter GetStatus(string username)
        {
            try
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
            } catch (Exception e)
            {
                throw e;
            } // End try catch
        } // End GetStatus

        /*
         * Name: ToggleStatus
         * Parameter: id(string)
         * Description: Account deactivation.
         */
        public void ToggleStatus(string id)
        {
            try
            {
                _context.Database.ExecuteSqlRaw($"ums_Delete_user '{id}'");
            } catch (Exception e)
            {
                throw e;
            } // End try catch
        } // End ToggleStatus

        /*
         * Name: UpdateName
         * Parameter: _account(Account)
         * Description: The user profile editing (Name).
         */
        public void UpdateName(Account _account)
        {
            try
            {
                // SQL text for execute procedure
                string sqlUpdateUser = $"ums_Update_name_user '{_account.acc_Id}', '{_account.acc_Firstname}', '{_account.acc_Lastname}'"; // Update name's user
                _context.Database.ExecuteSqlRaw(sqlUpdateUser);
            }
            catch (Exception e)
            {
                throw e;
            } // End try catch
        } // End UpdateName

        /*
         * Name: UpdateRole
         * Parameter: _account(Account)
         * Description: The user profile editing (Role).
         */
        public void UpdateRole(Account _account)
        {
            try
            {
                string sqlUpdateRoleUser = $"ums_Update_role_user '{_account.acc_Id}', '{_account.acc_Rolename}'"; // Update role's user
                _context.Database.ExecuteSqlRaw(sqlUpdateRoleUser);
            }
            catch (Exception e)
            {
                throw e;
            } // End try catch
        } // End UpdateRole
    } // End AccountRepository
}
