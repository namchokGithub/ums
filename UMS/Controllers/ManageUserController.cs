﻿using System;
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
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Identity;

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
                _logger.LogDebug(1, "NLog injected into EditProfileController.");
                _accountContext = accountContext;
                _logger.LogDebug(1, "Account Context injected into EditProfileController.");                
                _editaccountContext = editaccountContext;
                _logger.LogDebug(1, "Edit Profile Context injected into EditProfileController.");
                _signInManager = signInManager;
                _logger.LogDebug(1, "Sign In Manager injected into EditProfileController.");
                _logger.LogTrace("End EditProfileController Constructor.");
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message.ToString());
                _logger.LogTrace("End EditProfileController Constructor.");
            }// End try catch

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
                _logger.LogTrace("Start Index.");
                // Set defalut exception message
                TempData["nullException"] = null;
                TempData["SqlException"] = null;

                var UserId = User.FindFirstValue(ClaimTypes.NameIdentifier); // Get user ID
                if (UserId == null) throw new Exception("The user ID not found !.");
                ViewData["UserId"] = UserId;

                string sqltext = "EXEC [dbo].ums_get_all_active_user";
                // Query data from "dbo.Account" and Convert to List<Account>
                var user = _accountContext.Account.FromSqlRaw(sqltext).ToList<Account>();
                _logger.LogDebug("Get all active user.");
                if (user == null) throw new Exception("Calling a method on a null object reference.");

                // Send data to view Index.cshtml
                ViewData["User"] = user;
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

                string sqltext = $"EXEC [dbo].ums_getUserById '{id}'";
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
                    messages = message
                }; // Object for set alert
                _logger.LogDebug("Create new objectJSON.");
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
                if (HttpContext.Request.Form["acc_RoleId"].ToString() != "0")
                {
                    // Has condition in store procedure if equal zero or '' it's nothing happened
                    _account.acc_Rolename = HttpContext.Request.Form["acc_RoleId"].ToString();
                    _logger.LogDebug("Set role ID.");
                } // End if check role
                // Check if parametor is null
                if (_account.acc_Id == null || _account.acc_Id == "") throw new Exception("Calling a method on a null object reference.");
                if (_account == null) throw new Exception("Calling a method on a null object reference.");
                // Console.WriteLine(_account);
                if (ModelState.IsValid)
                {

                    // SQL text for execute procedure
                    string sqlUpdateUser = $"ums_updateUser '{_account.acc_Id}', '{_account.acc_Firstname}', '{_account.acc_Lastname}'"; // Update name's user
                    string sqlUpdateRoleUser = $"ums_updateRoleUser '{_account.acc_Id}', '{_account.acc_Rolename}'";              // Update role's user

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
                            TempData["UpdateResult"] = @"toastr.success('Success !')";
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
                    _logger.LogError("ModelState is not valid!");
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
                // Check if parametor is null
                if (id == null) throw new Exception("Calling a method on a null object reference.");

                // SQL text for execute procudure
                string sqlText = $"ums_deleteUser '{id}'";
                _accountContext.Database.ExecuteSqlRaw(sqlText);
                _logger.LogDebug("Inactive user.");

                var result = false;
                while (!result)
                {
                    try
                    {
                        _accountContext.SaveChanges();
                        _logger.LogDebug("Save changes: Inactive user.");
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
                _logger.LogTrace("Start check user is exist.");
                // Set parameter for get value
                var checkExits = new SqlParameter("@returnVal", SqlDbType.Int);
                checkExits.Direction = ParameterDirection.Output;

                // Return value from sture procudure
                var sqlText = $"EXEC @returnVal=[dbo].ums_Check_user '{userStr}'";
                _accountContext.Database.ExecuteSqlRaw(sqlText, checkExits);
                _logger.LogDebug("Check user is exist.");

                var result = false;
                while (!result)
                {
                    try
                    {
                        _accountContext.SaveChanges();
                        _logger.LogDebug("Save changes: Check user is exist.");
                        result = true; // If success
                    }
                    catch (Exception e)
                    {
                        _logger.LogError(e.Message.ToString());
                        TempData["Exception"] = @"Swal.fire({ icon: 'error', title: 'Error !', text: `" + e.Message + @"`, showConfirmButton: true })";
                    } // End try catch
                } // Check if check succeeded

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
    