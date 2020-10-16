using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using User_Management_System.Data;
using Microsoft.Extensions.Logging;
using User_Management_System.Models;
using Microsoft.AspNetCore.Authorization;

/*
 * Name: ManageUserController.cs
 * Author: Namchok Singhachai
 * Description: The controller manages user.
 */

namespace User_Management_System.Controllers
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
            _logger = logger;
            _unitOfWork = new UnitOfWork(context);
            _logger.LogTrace("Start manage user controller.");
        } // End constructor

        /*
         * Name: Index
         * Author: Namchok Singhachai
         * Description: Show all users currently active on the system.
         */
        public async Task<IActionResult> Index()
        {
            try
            {
                _logger.LogTrace("Start manage user index.");
                _logger.LogTrace("Finding user ID.");
                ViewData["UserId"] = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? throw new Exception("The user ID not found !.");  // Get user ID
                _logger.LogDebug("Getting all active users.");
                ViewData["User"] = await _unitOfWork.Account.GetAllAsync() ?? throw new Exception("Calling a method on a null object reference."); // Send data to view Index.cshtml
                _unitOfWork.Account.Dispose();
                _logger.LogTrace("End manage user index.");
                return View();
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message.ToString());
                TempData["nullException"] = @"Swal.fire({ icon: 'error', title: 'Error !', text: `" + e.Message.Replace("`", "'").Replace("\\", "/") + @"`, showConfirmButton: true });";
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
        public async Task<JsonResult> GetUser(string id)
        {
            try
            {
                _logger.LogTrace("Start get user.");
                if (id == null || id.ToString() == "") throw new Exception("Calling a method on a null object reference."); // Check if parameter is null
                _logger.LogInformation($"Getting user.");
                _logger.LogTrace("End get user.");
                return new JsonResult(await _unitOfWork.Account.GetByIDAsync(id) ?? throw new Exception("Calling a method on a null object reference.")); // Return JSON by Ajax
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message.ToString());
                _logger.LogTrace("End get user.");
                return new JsonResult(new ObjectJSON
                {
                    condition = "error",
                    messages = @"Swal.fire({ icon: 'error', title: 'Error !', text: `" + e.Message.Replace("\\", "/").Replace("`", "'") + @"`, showConfirmButton: true });",
                    text = e.Message.Replace("\\", "/").Replace("`", "'")
                });
            }
            finally
            {
                await _unitOfWork.Account.DisposeAsync();
            }// End try catch
        } // End get user

        /*
         * Name: EditUser
         * Parameter: _unitOfWork.Account(Account)
         * Author: Namchok Singhachai
         * Description: User profile editing.
         */
        [HttpPost]
        public async Task<IActionResult> EditUser(Management param_account)
        {
            try
            {
                _logger.LogTrace("Start user editing.");
                TempData["UpdateResult"] = null;
                param_account.acc_Id = HttpContext.Request.Form["acc_Id"].ToString();
                if (HttpContext.Request.Form["acc_RoleId"].ToString() != "0" || HttpContext.Request.Form["acc_RoleId"].ToString() != "")
                {
                    _logger.LogDebug("Setting role ID.");
                    param_account.acc_Rolename = HttpContext.Request.Form["acc_RoleId"].ToString(); // Has condition in store procedure if equal zero or '' it's nothing happened
                } // End checking role
                if (param_account.acc_Id == null || param_account.acc_Id == "") throw new Exception("Calling a method on a null object reference.");
                if (ModelState.IsValid)
                {
                    await _unitOfWork.Account.UpdateNameAsync(param_account);
                    await _unitOfWork.Account.UpdateRoleAsync(param_account);
                    var result = false;
                    while (!result)
                    {
                        try
                        {
                            await _unitOfWork.Account.CompleteAsync();
                            await _unitOfWork.Account.DisposeAsync();
                            _logger.LogDebug("Save changes: User successfully updated.");
                            TempData["UpdateResult"] = @"toastr.success('User successfully updated!');";
                            result = true;
                        }
                        catch (Exception e)
                        {
                            throw e;
                        } // End try catch
                    } // if update successfully
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
                TempData["UpdateResult"] = @"Swal.fire({ icon: 'error', title: 'Error !', text: `" + e.Message.Replace("\\", "/").Replace("`", "'") + @"`, showConfirmButton: true });";
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
        public async Task DeleteUser(string id)
        {
            try
            {
                _logger.LogTrace("Start account deactivation.");
                if (id == null || id.ToString() == "") throw new Exception("Calling a method on a null object reference.");
                _logger.LogDebug("Executing sql for user deactivation.");
                await _unitOfWork.Account.ToggleStatusAsync(id);
                var result = false;
                while (!result)
                {
                    try
                    {
                        await _unitOfWork.Account.CompleteAsync();
                        await _unitOfWork.Account.DisposeAsync();
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
                TempData["Exception"] = @"Swal.fire({ icon: 'error', title: 'Error !', text: `" + e.Message.Replace("\\", "/").Replace("`", "'") + @"`, showConfirmButton: true });";
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
        public async Task<int> CheckUserExist(string username = "", string status = "Y")
        {
            try
            {
                _logger.LogTrace("Start checking user.");
                if (username == null && status == null) throw new Exception("Calling a method on a null object reference.");
                SqlParameter checkExits = await _unitOfWork.Account.FindByUsernameAsync(username, status);
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
        public async Task<JsonResult> GetStatusUser(string username = "")
        {
            try
            {
                _logger.LogTrace("Start getting status user.");
                if (username == null || username.ToString() == "") throw new Exception("Calling a method on a null object reference.");
                var status = await _unitOfWork.Account.GetStatusAsync(username);
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
                _logger.LogTrace("End getting status user.");
                return new JsonResult(new ObjectJSON
                {
                    condition = "error",
                    messages = @"Swal.fire({ icon: 'error', title: 'Error !', text: `" + e.Message.Replace("\\", "/").Replace("`", "'") + @"`, showConfirmButton: true });",
                    text = e.Message.Replace("\\", "/").Replace("`", "'")
                });
            } // End try catch
        } // End GetStatusUser

        /*
         * Name: objectJSON
         * Author: Namchok Singhachai
         * Description: For create json object result to view and check response
         */
        public class ObjectJSON
        {
            public string condition { set; get; } // For check etc. success error and warning
            public string messages { set; get; } // Text explain
            public string text { set; get; } // Text explain
        } // End objectJSON
    } // End class
}
