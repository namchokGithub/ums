using System;
using UMS.Data;
using UMS.Models;
using System.Security.Claims;
using System.Threading.Tasks;
using UMS.Areas.Identity.Data;
using Microsoft.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Security.Cryptography;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Identity;
using System.Text.RegularExpressions;
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
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        /*
         * Name: ManageUserController
         * Parameter: context(AuthDbContext), logger(ILogger<ManageUserController>), signInManager(SignInManager<ApplicationUser>), userManager(UserManager<ApplicationUser>)
         * Author: Namchok Singhachai
         */
        public ManageUserController(AuthDbContext context, ILogger<ManageUserController> logger, SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
        {
            _logger = logger;
            _signInManager = signInManager;
            _userManager = userManager;
            _unitOfWork = new UnitOfWork(context);
            _logger.LogInformation("Start manage user controller.");
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
         * Name: EditProfile
         * Author: Wannapa Srijermtong
         * Description: Getting Firstname, Lastname and LoginProvider by user Id.
         */
        public async Task<IActionResult> EditProfile()
        {
            try
            {
                _logger.LogTrace("Start edit profile index.");
                var UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                ViewData["UserId"] = UserId ?? throw new Exception("User ID not found!.");
                _logger.LogDebug("Getting a user by ID.");
                ViewData["User"] = await _unitOfWork.Account.GetByIDAsync(UserId) ?? throw new Exception("Calling a method on a null object reference.");
                await _unitOfWork.Account.DisposeAsync();
                _logger.LogTrace("End edit profile controller index.");
                return View();
            }
            catch (Exception e)
            {
                TempData["EditProfileException"] = @"Swal.fire({ icon: 'error', title: 'Error !', text: `" + e.Message.Replace("\\", "/").Replace("`", "'") + @"`, showConfirmButton: true });";
                _logger.LogTrace("End edit profile index.");
                return View();
            } // End try catch
        } // End edit profile

        /*
         * Name: Edit
         * Author: Wannapa Srijermtong
         * Description: User profile editing.
         */
        [HttpPost]
        public async Task<IActionResult> Edit(Account account)
        {
            try
            {
                _logger.LogTrace("Start edit profile.");
                _logger.LogDebug("Getting value from httpcontext request.");
                if (account == null) throw new Exception("Calling a method on a null object reference (Account).");

                var acc_Id = account.acc_Id;
                var acc_Firstname = account.acc_Firstname;
                var acc_Lastname = account.acc_Lastname;
                var IsUpdatePassword = account.acc_IsActive.ToString(); 
                var acc_NewPassword = HttpContext.Request.Form["acc_NewPassword"];
                var acc_CurrentPassword = HttpContext.Request.Form["acc_CurrentPassword"];
                var acc_ConfirmPassword = HttpContext.Request.Form["acc_ConfirmPassword"]; // Get data from Form Input

                if (acc_Id.ToString() == null || acc_Id.ToString() == "") throw new Exception("Calling a method on a null object reference (ID).");

                _logger.LogDebug("Checking regular expression.");
                var RegExName = @"^[a-zA-Z]+(([a-zA-Z])?[a-zA-Z]*)*$";
                var RegExPassword = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[\:\;\[\]\\\|\/\.\,\'\""\()\{}\<>_#$!%@@^฿*?&\-\+\=])[A-Za-z0-9\:\;\[\]\\\|\/\.\,\'\""\()\{}\<>_#$!%@@^฿*?&\-\+\=]+$";

                if (!Regex.IsMatch(acc_Firstname, RegExName) && acc_Firstname != "" ||
                    !Regex.IsMatch(acc_Lastname, RegExName) && acc_Lastname != "" ||
                    !Regex.IsMatch(acc_CurrentPassword, RegExPassword) && acc_CurrentPassword != "" ||
                    !Regex.IsMatch(acc_NewPassword, RegExPassword) && acc_NewPassword != "" ||
                    !Regex.IsMatch(acc_ConfirmPassword, RegExPassword) && acc_ConfirmPassword != "")
                {
                    // Validation if acc_Firstname do not math with Regular expression.
                    if (!Regex.IsMatch(acc_Firstname, RegExName) && acc_Firstname != "")
                    {
                        TempData["EditProfileErrorResult"] = @"toastr.warning('The First name can not be blank and must only character.');";
                    }
                    else
                    // Validation if acc_Lastname do not math with Regular expression.
                    if (!Regex.IsMatch(acc_Lastname, RegExName) && acc_Lastname != "")
                    {
                        TempData["EditProfileErrorResult"] = @"toastr.warning('The Last name can not be blank and must only character.');";
                    }
                    else
                    // Validation if acc_CurrentPassword do not math with Regular expression.
                    if (!Regex.IsMatch(acc_CurrentPassword, RegExPassword) && acc_CurrentPassword != "")
                    {
                        TempData["EditProfileErrorResult"] = @"toastr.warning('The password must contain at least <br> 1 uppercase, 1 lowercase, 1 digit and 1 special character.');";
                    }
                    else
                    // Validation if acc_NewPassword do not math with Regular expression.
                    if (!Regex.IsMatch(acc_NewPassword, RegExPassword) && acc_NewPassword != "")
                    {
                        TempData["EditProfileErrorResult"] = @"toastr.warning('The password must contain at least <br> 1 uppercase, 1 lowercase, 1 digit and 1 special character.');";
                    }
                    else
                    // Validation if acc_ConfirmPassword do not math with Regular expression.
                    if (!Regex.IsMatch(acc_ConfirmPassword, RegExPassword) && acc_ConfirmPassword != "")
                    {
                        TempData["EditProfileErrorResult"] = @"toastr.warning('The password must contain at least <br> 1 uppercase, 1 lowercase, 1 digit and 1 special character.');";
                    }
                    _logger.LogTrace("End edit profile.");
                    return RedirectToAction("EditProfile", "ManageUser", new { id = acc_Id });
                } // End checking input

                // Checking user updating // f for update name user // t for update name and password user
                if (IsUpdatePassword.ToString() == "f")
                {
                    _logger.LogDebug("Updating name user.");
                    _unitOfWork.Account.UpdateName(new Management { acc_Id = acc_Id, acc_Firstname = acc_Firstname, acc_Lastname = acc_Lastname });
                    var resultUpdate_user = false;
                    while (!resultUpdate_user)
                    {
                        try
                        {
                            _unitOfWork.Account.Complete();
                            _unitOfWork.Account.Dispose();
                            _logger.LogInformation("Update successfully.");
                            TempData["EditProfileSuccessResult"] = @"toastr.success('User profile successfully updated!');";
                            resultUpdate_user = true; // If update successfully
                        }
                        catch (Exception e)
                        {
                            throw e;
                        } // End try catch
                    } // Check if update successfully

                    _logger.LogTrace("End edit profile.");
                    return RedirectToAction("EditProfile", "ManageUser", new { id = acc_Id });
                }
                else
                {
                    // Validation if acc_NewPassword and acc_ConfirmPassword do not match.
                    if (acc_NewPassword != acc_ConfirmPassword)
                    {
                        TempData["EditProfileErrorResult"] = @"toastr.warning('The new password and confirmation password do not match.');";
                        _logger.LogTrace("End edit profile.");
                        return RedirectToAction("Index", "EditProfile", new { id = acc_Id });
                    } // End checking password match

                    var result = await _signInManager.PasswordSignInAsync(User.Identity.Name, acc_CurrentPassword, false, lockoutOnFailure: false);
                    _logger.LogDebug("Signing in with password.");
                    if (!result.Succeeded)
                    {
                        _logger.LogWarning("The current password is incorrect!");
                        TempData["EditProfileErrorResult"] = @"toastr.warning('Current password is incorrect!');";
                        _logger.LogTrace("End edit profile.");
                        return RedirectToAction("EditProfile", "ManageUser", new { id = acc_Id });
                    }
                    else
                    {
                        if (acc_NewPassword == "") throw new Exception("New password is null.");
                        string acc_NewPasswordHashed = _userManager.PasswordHasher.HashPassword(new ApplicationUser
                        {
                            Id = account.acc_Id,
                            UserName = account.acc_User,
                            acc_Firstname = account.acc_Firstname,
                            acc_Lastname = account.acc_Lastname,
                            Email = account.acc_Email
                        }, acc_NewPassword);

                        // SQL text for execute procedure
                        _logger.LogDebug("Updating name user and password.");
                        await _unitOfWork.Account.UpdateNameAndPasswordAsync(new Management { acc_Id = acc_Id, acc_Firstname = acc_Firstname, acc_Lastname = acc_Lastname, acc_PasswordHash = acc_NewPasswordHashed });
                        var resultUpdate_user = false;
                        while (!resultUpdate_user)
                        {
                            try
                            {
                                await _unitOfWork.Account.CompleteAsync();
                                await _unitOfWork.Account.DisposeAsync();
                                _logger.LogInformation("Update successfully.");
                                TempData["EditProfileSuccessResult"] = @"toastr.success('User profile update successfully!');";
                                resultUpdate_user = true; // If update successful
                            }
                            catch (Exception e)
                            {
                                throw e;
                            } // End try catch
                        } // Check if update successfully
                    } // End if current password is correct
                }
                _logger.LogTrace("End edit profile.");
                return RedirectToAction("EditProfile", "ManageUser", new { id = acc_Id });
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message.ToString());
                TempData["Exception"] = @"Swal.fire({ icon: 'error', title: 'Error !', text: `" + e.Message.Replace("\\", "/").Replace("`", "'") + @"`, showConfirmButton: true });";
                _logger.LogTrace("End edit profile.");
                return RedirectToAction("Index", "Home");
            } // End try catch
        } // End Edit

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
