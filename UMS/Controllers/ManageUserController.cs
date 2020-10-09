﻿using System;
using UMS.Data;
using UMS.Models;
using System.Security.Claims;
using Microsoft.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;

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
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<ManageUserController> _logger;
        /*
         * Name: ManageUserController
         * Parameter: context(AuthDbContext), logger(ILogger<ManageUserController>)
         * Author: Namchok Singhachai
         */
        public ManageUserController(AuthDbContext context, ILogger<ManageUserController> logger)
        {
            try
            {
                _logger = logger;
                _unitOfWork = new UnitOfWork(context);
                _logger.LogTrace("Start manage user controller.");
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message.ToString());
                _logger.LogTrace("End manage user controller.");
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
                _logger.LogTrace("Finding user ID.");
                ViewData["UserId"] = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? throw new Exception("The user ID not found !.");  // Get user ID
                _logger.LogDebug("Getting all active users.");
                ViewData["User"] = _unitOfWork.Account.GetAll() ?? throw new Exception("Calling a method on a null object reference."); // Send data to view Index.cshtml
                _unitOfWork.Account.Dispose();
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
        public JsonResult GetUser(string id)
        {
            try
            {
                _logger.LogTrace("Start get user.");
                if (id == null || id.ToString() == "") throw new Exception("Calling a method on a null object reference."); // Check if parameter is null
                _logger.LogInformation($"Getting user by {id}.");
                _logger.LogTrace("End get user.");
                return new JsonResult(_unitOfWork.Account.GetByID(id) ?? throw new Exception("Calling a method on a null object reference.")); // Return JSON by Ajax
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message.ToString());
                _logger.LogTrace("End get user.");
                return new JsonResult(new objectJSON
                {
                    condition = "error",
                    messages = @"Swal.fire({ icon: 'error', title: 'Error !', text: `" + e.Message.Replace("\\", "/").Replace("`", "|") + @"`, showConfirmButton: true });",
                    text = e.Message.Replace("\\", "/")
                });
            }
            finally
            {
                _unitOfWork.Account.Dispose();
            }// End try catch
        } // End get user

        /*
         * Name: EditUser
         * Parameter: _account(Account)
         * Author: Namchok Singhachai
         * Description: User profile editing.
         */
        [HttpPost]
        public IActionResult EditUser(Account _account)
        {
            try
            {
                _logger.LogTrace("Start user editing.");
                TempData["UpdateResult"] = null;
                _account.acc_Id = HttpContext.Request.Form["acc_Id"].ToString();
                if (HttpContext.Request.Form["acc_RoleId"].ToString() != "0" || HttpContext.Request.Form["acc_RoleId"].ToString() != "")
                {
                    _logger.LogDebug("Setting role ID.");
                    _account.acc_Rolename = HttpContext.Request.Form["acc_RoleId"].ToString(); // Has condition in store procedure if equal zero or '' it's nothing happened
                } // End checking role
                if (_account.acc_Id == null || _account.acc_Id == "") throw new Exception("Calling a method on a null object reference.");
                if (ModelState.IsValid)
                {
                    _unitOfWork.Account.UpdateName(_account);
                    _unitOfWork.Account.UpdateRole(_account);
                    TempData["UpdateResult"] = @"toastr.success('Update user successfully!');";
                    var result = false;
                    while (!result)
                    {
                        try
                        {
                            _unitOfWork.Account.Complete();
                            _unitOfWork.Account.Dispose();
                            _logger.LogDebug("Save changes: User update successfully.");
                            TempData["UpdateResult"] = @"toastr.success('Update user successfully!');";
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
                    throw new Exception("ModelState is not valid!.");
                } // End checking model state.
                _logger.LogTrace("End user editing.");
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message.ToString());
                TempData["UpdateResult"] = @"Swal.fire({ icon: 'error', title: 'Error !', text: `" + e.Message.Replace("\\", "/") + @"`, showConfirmButton: true });";
                return RedirectToAction("Index");
            } // End try catch
        } // End EditUser

        /*
         * Name: DeleteUser
         * Parameter: id(string)
         * Author: Namchok Singhachai
         * Description: Account deactivation.
         */
        [HttpPost]
        public void DeleteUser(string id)
        {
            try
            {
                _logger.LogTrace("Start account deactivation.");
                if (id == null || id.ToString() == "") throw new Exception("Calling a method on a null object reference.");
                _logger.LogDebug("Executing sql for user deactivation.");
                _unitOfWork.Account.ToggleStatus(id);
                var result = false;
                while (!result)
                {
                    try
                    {
                        _unitOfWork.Account.Complete();
                        _unitOfWork.Account.Dispose();
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
        } // End DeleteUser

        /*
         * Name: CheckUserExist
         * Parameter: username(string), status(string)
         * Author: Namchok Singhachai
         * Description: Checking user is already exist on system.
         */
        [AllowAnonymous] // For register
        public int CheckUserExist(string username = "", string status = "Y")
        {
            try
            {
                _logger.LogTrace("Start checking user.");
                if (username == null && status == null) throw new Exception("Calling a method on a null object reference.");
                SqlParameter checkExits = _unitOfWork.Account.FindByUsername(username, status);
                _logger.LogDebug("Checking user.");
                _logger.LogInformation($"Detected {(int)checkExits.Value} users.");
                _logger.LogTrace("End check user is exist.");
                return (int)checkExits.Value;
            }
            catch (Exception e)
            {
                _logger.LogWarning(e.Message.ToString());
                _logger.LogTrace("End checking user.");
                throw new Exception(e.Message.ToString());
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
                var status = _unitOfWork.Account.GetStatus(username);
                if (status.Value == null) throw new Exception("Calling a method on a null object reference.");
                if (!int.TryParse(status.Value.ToString(), out _)) throw new Exception("Uncorrect type."); // If status if not integer
                if ((int)status.Value == 1) status.Value = "ACTIVE";
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
                    messages = @"Swal.fire({ icon: 'error', title: 'Error !', text: `" + e.Message.Replace("\\", "/").Replace("`", "|") + @"`, showConfirmButton: true });",
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
