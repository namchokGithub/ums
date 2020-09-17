using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using UMS.Areas.Identity.Data;
using UMS.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Data.SqlClient;
using System.Security.Claims;
using System.Data;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Identity;
using Microsoft.CodeAnalysis.FlowAnalysis;

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
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger<ManageUserController> _logger;

        /*
         * Name: ManageUserController
         * Parameter: accountContext(AccountContext)
         * Author: Namchok Singhachai
         * Description: Set context for database
         */
        public ManageUserController(AccountContext accountContext, EditAccountContext editaccountContext, ILogger<ManageUserController> logger, SignInManager<ApplicationUser> signInManager)
        {
            try
            {
                _logger = logger;
                _logger.LogTrace("NLog injected into EditProfileController.");
                _accountContext = accountContext;
                _logger.LogTrace("Account Context injected into EditProfileController.");                
                _editaccountContext = editaccountContext;
                _logger.LogTrace("Edit Profile Context injected into EditProfileController.");
                _signInManager = signInManager;
                _logger.LogTrace("Sign In Manager injected into EditProfileController.");
                _logger.LogTrace("Start EditProfileController Constructor.");
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message.ToString());
                _logger.LogTrace("End EditProfileController Constructor.");
            }// End try catch
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
                _logger.LogTrace("Start Index.");
                // Set defalut exception message
                TempData["nullException"] = null;
                TempData["SqlException"] = null;

                var UserId = User.FindFirstValue(ClaimTypes.NameIdentifier); // Get user ID
                _logger.LogTrace("Find first value from user.");
                ViewData["UserId"] = UserId ?? throw new Exception("The user ID not found !.");

                string sqltext = "EXEC [dbo].ums_Get_all_active_user"; // Set sql text for execute
                // Query data from "dbo.Account" and Convert to List<Account>
                var alluser = _accountContext.Account.FromSqlRaw(sqltext).ToList<Account>();
                _logger.LogDebug("Get all active user.");
                if (alluser == null) throw new Exception("Calling a method on a null object reference.");

                // Send data to view Index.cshtml
                ViewData["User"] = alluser;
                _logger.LogTrace("End Index.");
                return View();
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message.ToString());
                string message = @"Swal.fire({ icon: 'error', title: 'Error !', text: `" + e.Message + @"`, showConfirmButton: true })";
                TempData["nullException"] = message;
                _logger.LogTrace("End Index.");
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
                _logger.LogTrace("Start get user.");
                // Check if query is null
                if (id == null) throw new Exception("Calling a method on a null object reference.");
                string sqltext = $"EXEC [dbo].ums_Get_user_by_Id '{id}'";
                // Query data from "dbo.Account" and Convert to List<Account>
                var user = _editaccountContext.EditAccount.FromSqlRaw(sqltext).ToList().FirstOrDefault<EditAccount>();
                _logger.LogDebug("Get user by ID");
                if (user == null) throw new Exception("Calling a method on a null object reference.");
                _logger.LogTrace("End get user.");
                // Return JSON by Ajax
                return new JsonResult(user);
            } catch (Exception e)
            {
                _logger.LogError(e.Message.ToString());
                string message = @"Swal.fire({ icon: 'error', title: 'Error !', text: `" + e.Message + @"`, showConfirmButton: true })";
                var er = new objectJSON
                {
                    condition = "error",
                    messages = message,
                    text = e.Message
                }; // Object for set alert
                _logger.LogTrace("Create new objectJSON.");
                _logger.LogTrace("End get user.");
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
                _logger.LogTrace("Start edit user.");
                // Check if select role form selection in form
                _account.acc_Id = HttpContext.Request.Form["acc_Id"].ToString();
                if (HttpContext.Request.Form["acc_RoleId"].ToString() != "0" || HttpContext.Request.Form["acc_RoleId"].ToString() != "")
                {
                    _account.acc_Rolename = HttpContext.Request.Form["acc_RoleId"].ToString(); // Has condition in store procedure if equal zero or '' it's nothing happened
                    _logger.LogDebug("Set role ID.");
                } // End if check role
                if (_account.acc_Id == null || _account.acc_Id == "") throw new Exception("Cannot find ID.");
                if (_account == null) throw new Exception("Calling a method on a null object reference.");
                // Console.WriteLine(_account);
                if (ModelState.IsValid)
                {
                    // SQL text for execute procedure
                    string sqlUpdateUser = $"ums_Update_name_user '{_account.acc_Id}', '{_account.acc_Firstname}', '{_account.acc_Lastname}'"; // Update name's user
                    string sqlUpdateRoleUser = $"ums_Update_role_user '{_account.acc_Id}', '{_account.acc_Rolename}'";              // Update role's user
                    // Update Account add UserRoles
                    _editaccountContext.Database.ExecuteSqlRaw(sqlUpdateUser);
                    _logger.LogDebug("Update name'user.");
                    _editaccountContext.Database.ExecuteSqlRaw(sqlUpdateRoleUser);
                    _logger.LogDebug("Update role'user.");
                    // For check if update success 
                    var result = false;
                    while (!result)
                    {
                        try
                        {
                            _editaccountContext.SaveChanges();
                            _logger.LogDebug("Save changes: Update user.");
                            TempData["UpdateResult"] = @"toastr.success('Update user succeeded!.')";
                            result = true;
                        }
                        catch (Exception e)
                        {
                            _logger.LogError(e.Message.ToString());
                            TempData["Exception"] = @"Swal.fire({ icon: 'error', title: 'Error !', text: `" + e.Message + @"`, showConfirmButton: true })";
                            _logger.LogTrace("End edit user.");
                            return View();
                        } // End try catch
                    } // if execute succeeded
                }
                else
                {
                    // return BadRequest(ModelState);
                    _logger.LogError("ModelState is not valid!.");
                    TempData["UpdateResult"] = @"Swal.fire({ icon: 'error', title: 'Error !', text 'ModelState is not valid!.', showConfirmButton: true })";
                } // End if-else
                _logger.LogTrace("End edit user.");
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message.ToString());
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
                _logger.LogTrace("Start delete user.");
                if (id == null) throw new Exception("Calling a method on a null object reference.");
                string sqlText = $"ums_Delete_user '{id}'";
                _accountContext.Database.ExecuteSqlRaw(sqlText);
                _logger.LogDebug("Execute sql inactive user.");
                var result = false;
                while (!result)
                {
                    try
                    {
                        _accountContext.SaveChanges();
                        _logger.LogDebug("Save changes: Inactive user.");
                        _logger.LogTrace("Inactive user succeeded.");
                        result = true; // If success
                    }
                    catch (Exception e)
                    {
                        _logger.LogError(e.Message.ToString());
                        TempData["Exception"] = @"Swal.fire({ icon: 'error', title: 'Error !', text: `" + e.Message + @"`, showConfirmButton: true })";
                        _logger.LogTrace("End delete user.");
                    } // End try catch
                } // Check if update succeeded
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message.ToString());
                // Send alert to home pages
                TempData["Exception"] = @"Swal.fire({ icon: 'error', title: 'Error !', text: `" + e.Message + @"`, showConfirmButton: true })";
                _logger.LogTrace("End delete user.");
            } // End try catch
        } // End deleteUser

        /*
         * Name: CheckUserExist
         * Parameter: userStr(string)
         * Author: Namchok Singhachai
         * Description: Check if user is exist and return 1
         */
        [AllowAnonymous] // For register
        public int CheckUserExist(string userStr = "", string status = "Y")
        {
            try
            {
                _logger.LogTrace("Start check user is exist.");
                if (userStr == null) throw new Exception("Calling a method on a null object reference.");
                var checkExits = new SqlParameter("@returnVal", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Output
                }; // Set parameter for get value
                var sqlText = $"EXEC @returnVal=[dbo].ums_Check_user '{userStr}', '{status}'";// Return value from sture procudure
                _accountContext.Database.ExecuteSqlRaw(sqlText, checkExits);
                _logger.LogDebug("Check user is exist.");
                var result = false;
                while (!result)
                {
                    try
                    {
                        _accountContext.SaveChanges();
                        _logger.LogDebug("Save changes: Check user is exist.");
                        _logger.LogTrace("Query succeeded.");
                        result = true; // If success
                    }
                    catch (Exception e)
                    {
                        _logger.LogError(e.Message.ToString());
                        TempData["Exception"] = @"Swal.fire({ icon: 'error', title: 'Error !', text: `" + e.Message + @"`, showConfirmButton: true })";
                    } // End try catch
                } // Check if check succeeded
                _logger.LogTrace($"User is exist {(int)checkExits.Value} items.");
                _logger.LogTrace("End check user is exist.");
                return (int)checkExits.Value;
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message.ToString());
                // Send alert to home pages
                TempData["Exception"] = @"Swal.fire({ icon: 'error', title: 'Error !', text: `" + e.Message + @"`, showConfirmButton: true })";
                _logger.LogTrace("End check user is exist.");
                return 0;
            } // End try catch
        } // End CheckUserExist

        /*
         * Name: GetStatusUser
         * Parameter: username(string)
         * Author: Namchok Singhachai
         * Description: Get status of user and check if exist.
         */
        [AllowAnonymous] // For register
        public JsonResult GetStatusUser(string username = "")
        {
            try
            {
                _logger.LogTrace("Start Get status user.");
                if (username == null) throw new Exception("Calling a method on a null object reference.");
                var status = new SqlParameter("@paramout_status", SqlDbType.Int)
                {
                    IsNullable = true,
                    Direction = ParameterDirection.Output,
                    Size = 10,
                    Value = DBNull.Value
                }; // Set parameter for get value
                string sqlGetStatusUser = $@"EXEC @paramout_status=[dbo].ums_Get_status_user '{username}'";
                _logger.LogTrace($"Executing sql stored procedure ({sqlGetStatusUser}).");
                _accountContext.Database.ExecuteSqlRaw(sqlGetStatusUser, status);
                if (status.Value == null) throw new Exception("Calling a method on a null object reference.");
                if (!int.TryParse(status.Value.ToString(), out _)) throw new Exception("Uncorrect type.");
                if((int)status.Value == 1) status.Value = "ACTIVE";
                else if ((int)status.Value == 0) status.Value = "INACTIVE";
                _logger.LogTrace("End Get status user.");
                return new JsonResult(status.Value);
            } catch (Exception e)
            {
                _logger.LogError(e.Message.ToString());
                string message = @"Swal.fire({ icon: 'error', title: 'Error !', text: `" + e.Message + @"`, showConfirmButton: true })";
                var er = new objectJSON
                {
                    condition = "error",
                    messages = message,
                    text = e.Message
                }; // Object for set alert
                _logger.LogTrace(message: "Create new objectJSON.");
                _logger.LogTrace("End Get status user.");
                return new JsonResult(er);
            }
        } // End GetStatusUser

        /*
         * Name: objectJSON
         * Author: Namchok Snghachai
         * Description: For create json object result to view and check response
         */
        public class objectJSON
        {
            public string condition { set; get; } // For check etc. success error and warning
            public string messages { set; get; } // Text explain
            public string text { set; get; } // Text explain
        } // End objectJSON
    } // End class
}
    