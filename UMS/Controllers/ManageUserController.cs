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
using Microsoft.AspNetCore.Authorization;
using Microsoft.Data.SqlClient;
using System.Security.Claims;
using System.Data;

/*
 * Name: MangeUserController.cs
 * Namespace: Controllers
 * Author: Namchok
 */

namespace UMS.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ManageUserController : Controller
    {
        // For context of database
        private readonly AccountContext _accountContext;
        private readonly EditAccountContext _editaccountContext;
        
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
        } // End constructor

        /*
         * Name: Index
         * Parameter: none
         * Author: Namchok Singhachai
         * Description: Show all users is active in the system
         */
        public IActionResult Index()
        {
            try
            {
                // Set defalut exception message
                TempData["nullException"] = null;
                TempData["SqlException"] = null;

                var UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                // Set Data to view
                if (UserId == null) throw new Exception("The user ID not found !.");
                ViewData["UserId"] = UserId;

                // SQL text for exextut procedure
                string sqltext = "EXEC [dbo].ums_get_all_active_user";

                // Query data from "dbo.Account" and Convert to List<Account>
                var user = _accountContext.Account.FromSqlRaw(sqltext).ToList<Account>();
                
                // Check if query is null
                if (user == null) throw new Exception("Calling a method on a null object reference");

                // Send data to view Index.cshtml
                ViewData["User"] = user;
                return View();
            }
            catch (Exception e)
            {
                // Set sweet alert with error messages
                string message = @"Swal.fire({ icon: 'error', title: 'Error !', text: `" + e.Message + @"`, showConfirmButton: true })";
                // Send alert to home pages
                TempData["nullException"] = message;
                return View();
            } // End try catch

        } // End index

        /*
         * Name: getUser
         * Parameter: id(string)
         * Author: Namchok Singhachai
         * Description: Get Active user by ID
         */
        [HttpPost]
        public JsonResult getUser(string id)
        {
            try
            {
                // Check if query is null
                if (id == null) throw new Exception("Calling a method on a null object reference");

                // SQL text for execute procedure
                string sqltext = $"EXEC [dbo].ums_getUserById '{id}'";

                // Query data from "dbo.Account" and Convert to List<Account>
                var user = _editaccountContext.EditAccount.FromSqlRaw(sqltext).ToList().FirstOrDefault<EditAccount>();

                // Check if query is null
                if (user == null) throw new Exception("Calling a method on a null object reference");

                // Return JSON by Ajax
                return new JsonResult(user);

            } catch (Exception e)
            {
                // Set sweet alert with error messages
                string message = @"Swal.fire({ icon: 'error', title: 'Error !', text: `" + e.Message + @"`, showConfirmButton: true })";

                var er = new objectJSON
                {
                    condition = "error",
                    messages = message
                }; // Object for set alert

                return new JsonResult(er);
            } // End try catch
        } // End get user
        
        /*
         * Name: editUser
         * Parameter: _account(EditAccount)
         * Author: Namchok Singhachai
         * Description: Edit profile user
         */
        [HttpPost]
        public IActionResult editUser(EditAccount _account)
        {
            try
            {
                // Check if parametor is null
                if (_account == null) throw new Exception("Calling a method on a null object reference.");

                // Check if select role form selection in form
                if (HttpContext.Request.Form["acc_RoleId"].ToString() != "0")
                {
                    // Has condition in store procedure if equal zero or '' it's nothing happened
                    _account.acc_Rolename = HttpContext.Request.Form["acc_RoleId"].ToString();
                } // End if check role

                // Console.WriteLine(_account);
                if (ModelState.IsValid)
                {

                    // SQL text for execute procedure
                    string sqlUpdateUser = $"ums_updateUser '{_account.acc_Id}', '{_account.acc_Firstname}', '{_account.acc_Lastname}'"; // Update name's user
                    string sqlUpdateRoleUser = $"ums_updateRoleUser '{_account.acc_Id}', '{_account.acc_Rolename}'";              // Update role's user

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
                            TempData["UpdateResult"] =
                                @"toastr.success('Success !')";
                            result = true;
                        }
                        catch (Exception e)
                        {
                            // Send alert to home pages
                            TempData["Exception"] = @"Swal.fire({ icon: 'error', title: 'Error !', text: `" + e.Message + @"`, showConfirmButton: true })";
                            return View();
                        } // End try catch
                    }

                }
                else
                {
                    // return BadRequest(ModelState);
                    TempData["UpdateResult"] = @"Swal.fire({ icon: 'error', title: 'Error !', showConfirmButton: true })";
                } // End if-else

                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                // Send alert to home pages
                TempData["UpdateResult"] = @"Swal.fire({ icon: 'error', title: 'Error !', text: `" + e.Message + @"`, showConfirmButton: true })";
                return RedirectToAction("Index");
            } // End try catch
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
            try
            {
                // Check if parametor is null
                if (id == null) throw new Exception("Calling a method on a null object reference");

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
                    catch (Exception e)
                    {
                        // Send alert to home pages
                        TempData["Exception"] = @"Swal.fire({ icon: 'error', title: 'Error !', text: `" + e.Message + @"`, showConfirmButton: true })";
                    } // End try catch
                }
            }
            catch (Exception e)
            {
                // Send alert to home pages
                TempData["Exception"] = @"Swal.fire({ icon: 'error', title: 'Error !', text: `" + e.Message + @"`, showConfirmButton: true })";
            } // End try catch
        } // End deleteUser

        /*
         * Name: checkUserExist
         * Parameter: user(string)
         * Author: Namchok Singhachai
         * Description: Check if user is exist and return 1
         */
        [AllowAnonymous] // For register
        public int checkUserExist(string userStr = "")
        {
            try
            {
                // Set parameter for get value
                var checkExits = new SqlParameter("@returnVal", SqlDbType.Int);
                checkExits.Direction = ParameterDirection.Output;

                // Return value from sture procudure
                var sqlText = $"EXEC @returnVal=[dbo].ums_Check_user '{userStr}'";
                _accountContext.Database.ExecuteSqlRaw(sqlText, checkExits);

                return (int)checkExits.Value;
            }
            catch (Exception e)
            {
                // Send alert to home pages
                TempData["Exception"] = @"Swal.fire({ icon: 'error', title: 'Error !', text: `" + e.Message + @"`, showConfirmButton: true })";
                return 0;
            } // End try catch
        } // End checkUserExist

        /*
         * Name: objectJSON
         * Author: Namchok Snghachai
         * Description: For create json object result to view and check response
         */
        class objectJSON
        {
            public string condition { set; get; } // For check etc. success error and warning
            public string messages { set; get; } // Text explain
        } // End objectJSON
    } // End class
}
    