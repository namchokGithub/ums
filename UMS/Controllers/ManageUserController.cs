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
 * Name: ManageUserController.cs
 * Author: Namchok Singhachai
 * Description: The controller manages user.
 */

namespace UMS.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ManageUserController : Controller
    {
        private readonly AccountContext _accountContext;
        private readonly EditAccountContext _editaccountContext;
        private readonly ILogger<ManageUserController> _logger;
        /*
         * Name: ManageUserController
         * Parameter: accountContext(AccountContext)
         * Author: Namchok Singhachai
         * Description: The constructor for set context for database.
         */
        public ManageUserController(AccountContext accountContext, EditAccountContext editaccountContext, ILogger<ManageUserController> logger)
        {
            try
            {
                _logger = logger;
                _accountContext = accountContext;
                _editaccountContext = editaccountContext;
                _logger.LogTrace("Start manageUser controller.");
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message.ToString());
                _logger.LogTrace("End manageUser controller.");
            }// End try catch
        } // End constructor

        /*
         * Name: Index
         * Author: Namchok Singhachai
         * Description: Show all users currently active on the system.
         */
        public IActionResult Index()
        {
            try
            {
                _logger.LogTrace("Start manage user index.");
                TempData["nullException"] = null;
                TempData["SqlException"] = null; // Set defalut exception message
                _logger.LogTrace("Finding user ID.");
                ViewData["UserId"] = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? throw new Exception("The user ID not found !");  // Get user ID
                _logger.LogDebug("Getting all active users.");
                string sqltext = "EXEC [dbo].ums_Get_all_active_user"; // Set sql text for execute
                var alluser = _accountContext.Account.FromSqlRaw(sqltext).ToList<Account>();
                ViewData["User"] = alluser ?? throw new Exception("Calling a method on a null object reference."); // Send data to view Index.cshtml
                _logger.LogTrace("End manage user index.");
                return View();
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message.ToString());
                TempData["nullException"] = @"Swal.fire({ icon: 'error', title: 'Error !', text: `" + e.Message.Replace("\\", "/") + @"`, showConfirmButton: true });";
                _logger.LogTrace("End manage user index.");
                return View();
            } // End try catch
        } // End index

        /*
         * Name: getUser
         * Parameter: id(string)
         * Author: Namchok Singhachai
         * Description: Getting a user is already active on the system.
         */
        [HttpPost]
        public JsonResult getUser(string id)
        {
            try
            {
                _logger.LogTrace("Start get user.");
                if (id == null || id.ToString() == "") throw new Exception("Calling a method on a null object reference."); // Check if parameter is null
                string sqltext = $"EXEC [dbo].ums_Get_user_by_Id '{id}'";
                var user = _editaccountContext.EditAccount.FromSqlRaw(sqltext).ToList().FirstOrDefault<EditAccount>();
                _logger.LogDebug("Getting user by ID.");
                if (user == null) throw new Exception("Calling a method on a null object reference.");
                _logger.LogTrace("End get user.");
                return new JsonResult(user); // Return JSON by Ajax
            } catch (Exception e)
            {
                _logger.LogError(e.Message.ToString());
                var er = new objectJSON
                {
                    condition = "error",
                    messages = @"Swal.fire({ icon: 'error', title: 'Error !', text: `" + e.Message.Replace("\\", "/") + @"`, showConfirmButton: true });",
                    text = e.Message
                }; // Object for set alert
                _logger.LogTrace("End get user.");
                return new JsonResult(er);
            } // End try catch
        } // End get user

        /*
         * Name: editUser
         * Parameter: _account(EditAccount)
         * Author: Namchok Singhachai
         * Description: User profile editing.
         */
        [HttpPost]
        public IActionResult editUser(EditAccount _account)
        {
            try
            {
                _logger.LogTrace("Start user editing.");
                _account.acc_Id = HttpContext.Request.Form["acc_Id"].ToString();
                if (HttpContext.Request.Form["acc_RoleId"].ToString() != "0" || HttpContext.Request.Form["acc_RoleId"].ToString() != "")
                {
                    _logger.LogDebug("Setting role ID.");
                    _account.acc_Rolename = HttpContext.Request.Form["acc_RoleId"].ToString(); // Has condition in store procedure if equal zero or '' it's nothing happened
                } // End checking role
                if (_account.acc_Id == null || _account.acc_Id == "") throw new Exception("Calling a method on a null object reference.");
                if (ModelState.IsValid)
                {
                    // SQL text for execute procedure
                    string sqlUpdateUser = $"ums_Update_name_user '{_account.acc_Id}', '{_account.acc_Firstname}', '{_account.acc_Lastname}'"; // Update name's user
                    string sqlUpdateRoleUser = $"ums_Update_role_user '{_account.acc_Id}', '{_account.acc_Rolename}'";              // Update role's user
                    // Update Account add UserRoles
                    _logger.LogDebug("Updating name user.");
                    _editaccountContext.Database.ExecuteSqlRaw(sqlUpdateUser);
                    _logger.LogDebug("Updating role user.");
                    _editaccountContext.Database.ExecuteSqlRaw(sqlUpdateRoleUser);
                    var result = false;
                    while (!result)
                    {
                        try
                        {
                            _editaccountContext.SaveChanges();
                            _logger.LogDebug("Save changes: User update successfully.");
                            TempData["UpdateResult"] = @"toastr.success('Update user successfully!')";
                            result = true;
                        }
                        catch (Exception e)
                        {
                            throw e;
                        } // End try catch
                    } // if update successful
                }
                else
                {
                    // return BadRequest(ModelState);
                    _logger.LogError("ModelState is not valid!.");
                    TempData["UpdateResult"] = @"Swal.fire({ icon: 'error', title: 'Error !', text 'ModelState is not valid!.', showConfirmButton: true });";
                } // End if-else
                _logger.LogTrace("End user editing.");
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message.ToString());
                TempData["UpdateResult"] = @"Swal.fire({ icon: 'error', title: 'Error !', text: `" + e.Message.Replace("\\", "/") + @"`, showConfirmButton: true });";
                return RedirectToAction("Index");
            } // End try catch
        } // End editUser

        /*
         * Name: deleteUser
         * Parameter: id(string)
         * Author: Namchok Singhachai
         * Description: Account deactivation.
         */
        [HttpPost]
        public void deleteUser(string id)
        {
            try
            {
                _logger.LogTrace("Start account deactivation.");
                if (id == null || id.ToString() == "") throw new Exception("Calling a method on a null object reference.");
                _logger.LogDebug("Executing sql for user deactivation.");
                string sqlText = $"ums_Delete_user '{id}'";
                _accountContext.Database.ExecuteSqlRaw(sqlText);
                var result = false;
                while (!result)
                {
                    try
                    {
                        _accountContext.SaveChanges();
                        _logger.LogTrace("Deactivation successful.");
                        result = true; // If deactivation successful
                    }
                    catch (Exception e)
                    {
                        throw e;
                    } // End try catch
                } // Check if deactivation successful
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message.ToString());
                TempData["Exception"] = @"Swal.fire({ icon: 'error', title: 'Error !', text: `" + e.Message.Replace("\\", "/") + @"`, showConfirmButton: true });";
                _logger.LogTrace("End account deactivation.");
            } // End try catch
        } // End deleteUser

        /*
         * Name: CheckUserExist
         * Parameter: userStr(string), status(string)
         * Author: Namchok Singhachai
         * Description: Checking user is already exist on system.
         */
        [AllowAnonymous] // For register
        public int CheckUserExist(string userStr = "", string status = "Y")
        {
            try
            {
                _logger.LogTrace("Start checking user.");
                if (userStr == null && status == null) throw new Exception("Calling a method on a null object reference.");
                var checkExits = new SqlParameter("@returnVal", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Output
                }; // Set parameter for get value
                var sqlText = $"EXEC @returnVal=[dbo].ums_Check_user '{userStr}', '{status}'";// Return value from sture procudure
                _accountContext.Database.ExecuteSqlRaw(sqlText, checkExits);
                _logger.LogDebug("Checking user.");
                var result = false;
                while (!result)
                {
                    try
                    {
                        _accountContext.SaveChanges();
                        _logger.LogTrace("Query successfully.");
                        result = true; // If successfully
                    }
                    catch (Exception e)
                    {
                        throw e;
                    } // End try catch
                } // If checking successfully
                _logger.LogDebug($"Detected {(int)checkExits.Value} users.");
                _logger.LogTrace("End check user is exist.");
                return (int)checkExits.Value;
            }
            catch (Exception e)
            {
                _logger.LogWarning(e.Message.ToString());
                _logger.LogTrace("End checking user.");
                return 0;
            } // End try catch
        } // End CheckUserExist

        /*
         * Name: GetStatusUser
         * Parameter: username(string)
         * Author: Namchok Singhachai
         * Description: Getting status of user is already exist on system.
         */
        [AllowAnonymous] // For register
        public JsonResult GetStatusUser(string username = "")
        {
            try
            {
                _logger.LogTrace("Start getting status user.");
                if (username == null || username.ToString() == "") throw new Exception("Calling a method on a null object reference.");
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
                if (!int.TryParse(status.Value.ToString(), out _)) throw new Exception("Uncorrect type."); // If status if not integer
                if((int)status.Value == 1) status.Value = "ACTIVE";
                else if ((int)status.Value == 0) status.Value = "INACTIVE";
                _logger.LogTrace("End getting status user.");
                return new JsonResult(status.Value);
            } 
            catch (Exception e)
            {
                _logger.LogError(e.Message.ToString());
                var er = new objectJSON
                {
                    condition = "error",
                    messages = @"Swal.fire({ icon: 'error', title: 'Error !', text: `" + e.Message.Replace("\\", "/") + @"`, showConfirmButton: true });",
                    text = e.Message.Replace("\\", "/")
                }; // Object for set alert
                _logger.LogTrace("End getting status user.");
                return new JsonResult(er);
            } // End try catch
        } // End GetStatusUser

        /*
         * Name: objectJSON
         * Author: Namchok Singhachai
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
    