using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using UMS.Areas.Identity.Data;
using UMS.Models;
using Microsoft.AspNetCore.Diagnostics;

/*
 * Name: MangeUserController.cs
 * Namespace: Controllers
 * Author: Namchok
 */

namespace UMS.Controllers
{
    public class ManageUserController : Controller
    {
        // For context of database
        private readonly AccountContext _accountContext;
        private readonly EditAccountContext _editaccountContext;
        public Account _account;

        /*
         * Name: ManageUserController
         * Parameter: accountContext(AccountContext)
         * Author: Namchok Singhachai
         * Description: Set context for database
         */
        public ManageUserController(AccountContext accountContext, EditAccountContext editaccountContext)
        {
            _accountContext = accountContext;
            _editaccountContext = editaccountContext;
        }

        /*
         * Name: Index
         * Parameter: none
         * Author: Namchok Singhachai
         * Description: Show all users is active in the system
         */
        public IActionResult Index()
        {
            // SQL text for exextut procedure
            string sqltext = "EXEC [dbo].ums_get_all_active_user";

            // Query data from "dbo.Account" and Convert to List<Account>
            var user = _accountContext.Account.FromSqlRaw(sqltext).ToList<Account>();

            // Send data to view Index.cshtml
            ViewData["User"] = user;
            return View();
        }

        /*
         * Name: getUser
         * Parameter: id(string)
         * Author: Namchok Singhachai
         * Description: Get Active user by ID
         */
        [HttpPost]
        public JsonResult getUser(string id)
        {
            // SQL text for execute procedure
            string sqltext = $"EXEC [dbo].ums_getUserById '{id}'";

            // Query data from "dbo.Account" and Convert to List<Account>
            var user = _editaccountContext.EditAccount.FromSqlRaw(sqltext).ToList<EditAccount>().FirstOrDefault<EditAccount>();

            return new JsonResult(user);
        }
        
        /*
         * Name: editUser
         * Parameter: none
         * Author: Namchok Singhachai
         * Description: Edit profile user
         */
        [HttpPost]
        public IActionResult editUser()
        {
            // Value for update
            var acc_Id  = HttpContext.Request.Form["acc_Id"];               // ID
            var acc_Firstname = HttpContext.Request.Form["acc_Firstname"];  // First name 
            var acc_Lastname = HttpContext.Request.Form["acc_Lastname"];    // Last name
            var acc_RoleId = HttpContext.Request.Form["acc_RoleId"];      // Role ID 

            // SQL text for execute procedure
            string sqlUpdateUser = $"ums_updateUser '{acc_Id}', '{acc_Firstname}', '{acc_Lastname}'"; // Update name's user
            string sqlUpdateRoleUser = $"ums_updateRoleUser '{acc_Id}', '{acc_RoleId}'";              // Update role's user

            // Update Account add UserRoles
            _editaccountContext.Database.ExecuteSqlRaw(sqlUpdateUser);
            _editaccountContext.Database.ExecuteSqlRaw(sqlUpdateRoleUser);

            // For check if update success 
            var result = false;
            while (!result)
            {
                try
                {
                    _editaccountContext.SaveChanges();
                    TempData["UpdateResult"] = @"Swal.fire({
                                                    icon: 'success',
                                                    title: 'Successed !',
                                                    showConfirmButton: false,
                                                    timer: 1000
                                                })";
                    result = true;
                } catch
                {
                    throw;
                }
            }

            return RedirectToAction("Index");
        } // End editUser

        /*
         * Name: deleteUser
         * Parameter: id(string)
         * Author: Namchok Singhachai
         * Description: Inactive account
         */
        [HttpPost]
        public void deleteUser(string id)
        {
            // SQL text for execute procudure
            string sqlText = $"ums_deleteUser '{id}'";
            // Inactive account by store procedure
            _accountContext.Database.ExecuteSqlRaw(sqlText);

            // For check if inactive success 
            var result = false;
            while (!result)
            {
                try
                {
                    _accountContext.SaveChanges();
                    result = true; // If success
                }
                catch
                {
                    throw;
                }
            }
        } // End deleteUser

    } // End class
}
    