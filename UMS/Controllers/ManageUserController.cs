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
            catch (SqlException ex)
            {
                string errorMessages = "";
                for (int i = 0; i < ex.Errors.Count; i++)
                {
                    errorMessages += "Index #" + i + "\n" +
                                        "Message: " + ex.Errors[i].Message + "\n" +
                                        "LineNumber: " + ex.Errors[i].LineNumber + "\n" +
                                        "Source: " + ex.Errors[i].Source + "\n" +
                                        "Procedure: " + ex.Errors[i].Procedure + "\n";
                }

                Console.WriteLine(errorMessages);   
                // Set sweet alert with error messages
                string message = @"Swal.fire({ icon: 'error', title: 'Error !', text: `" + errorMessages + @"`, showConfirmButton: true })";
                // Send alert to home pages
                TempData["SqlException"] = message;
                return View();
            }
            catch (Exception e)
            {
                // Set sweet alert with error messages
                string message = @"Swal.fire({ icon: 'error', title: 'Error !', text: '" + e.Message + @"', showConfirmButton: true })";
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
                string message = @"Swal.fire({ icon: 'error', title: 'Error !', text: '" + e.Message + @"', showConfirmButton: true })";

                var er = new objectJason
                {
                    condition = "error",
                    messages = message
                };

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

            // Check if select role form selection in form
            if (HttpContext.Request.Form["acc_RoleId"].ToString() != "0")
            {
                // Has condition in store procedure if equal zero or '' it's nothing happened
                _account.acc_Rolename = HttpContext.Request.Form["acc_RoleId"].ToString();
            }

            Console.WriteLine(_account);
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
                        TempData["UpdateResult"] = @"Swal.fire({
                                                    icon: 'success',
                                                    title: 'Successed !',
                                                    showConfirmButton: false,
                                                    timer: 1000
                                                })";
                        result = true;
                    }
                    catch
                    {
                        throw;
                    }
                }

            } else
            {
                TempData["UpdateResult"] = @"Swal.fire({
                                                    icon: 'error',
                                                    title: 'Error !',
                                                    showConfirmButton: true
                                            })";
                // return BadRequest(ModelState);
            } // End if-else

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

        /*
         * Name: objectJason
         * Author: Namchok Snghachai
         * Description: For create json object result to view and check response
         */
        class objectJason
        {
            public string condition { set; get; } // For check etc. success error and warning
            public string messages { set; get; } // Text explain
        }
    } // End class
}
    