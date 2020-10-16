using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using User_Management_System.Data;
using Microsoft.Extensions.Logging;
using System.Security.Cryptography;
using User_Management_System.Models;
using Microsoft.AspNetCore.Identity;
using System.Text.RegularExpressions;
using User_Management_System.Areas.Identity.Data;

/*
 * Name: EditProfileController.cs
 * Author: Wannapa Srijermtong
 * Description: The controller manages an user information.
 */

namespace User_Management_System.Controllers
{
    public class EditProfileController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<EditProfileController> _logger;
        private readonly SignInManager<ApplicationUser> _signInManager;
        /*
         * Name: EditProfileController
         * Parameter: context(AuthDbContext), signInManager(SignInManager<ApplicationUser>) , logger(ILogger<EditProfileController>)
         * Author: Wannapa Srijermtong
         */
        public EditProfileController(AuthDbContext context, SignInManager<ApplicationUser> signInManager, ILogger<EditProfileController> logger)
        {
            _logger = logger;
            _signInManager = signInManager;
            _unitOfWork = new UnitOfWork(context);
            _logger.LogTrace("Start editProfile controller.");
        } // End Constructor

        /*
         * Name: Index
         * Author: Wannapa Srijermtong
         * Description: Getting Firstname, Lastname and LoginProvider by user Id.
         */
        public async Task<IActionResult> Index()
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
        } // End Index

        /*
         * Name: EditProfile
         * Author: Wannapa Srijermtong
         * Description: Edit user profile
         */
        [HttpPost]
        public async Task<IActionResult> EditProfile()
        {
            try
            {
                _logger.LogTrace("Start edit profile.");
                _logger.LogDebug("Getting value from httpcontext request.");
                
                var acc_Id = HttpContext.Request.Form["acc_Id"];
                var acc_Lastname = HttpContext.Request.Form["acc_Lastname"];
                var acc_Firstname = HttpContext.Request.Form["acc_Firstname"];
                var acc_NewPassword = HttpContext.Request.Form["acc_NewPassword"];
                var acc_CurrentPassword = HttpContext.Request.Form["acc_CurrentPassword"];
                var acc_ConfirmPassword = HttpContext.Request.Form["acc_ConfirmPassword"];
                var IsUpdatePassword = HttpContext.Request.Form["acc_IsActive"].ToString(); // Get data from Form Input

                if (acc_Id.ToString() == null || acc_Id.ToString() == "") throw new Exception("Calling a method on a null object reference.");

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
                    return RedirectToAction("Index", "EditProfile", new { id = acc_Id });
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
                    return RedirectToAction("Index", "EditProfile", new { id = acc_Id });
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
                        return RedirectToAction("Index", "EditProfile", new { id = acc_Id });
                    }
                    else
                    {
                        if (acc_NewPassword == "") throw new Exception("New password is null.");

                        _logger.LogDebug("Hasing a password.");
                        // Change acc_NewPassword to Password Hash
                        byte[] salt;
                        byte[] buffer2;
                        using (Rfc2898DeriveBytes bytes = new Rfc2898DeriveBytes(acc_NewPassword, 0x10, 0x3e8))
                        {
                            salt = bytes.Salt;
                            buffer2 = bytes.GetBytes(0x20);
                        }
                        byte[] dst = new byte[0x31];
                        Buffer.BlockCopy(salt, 0, dst, 1, 0x10);
                        Buffer.BlockCopy(buffer2, 0, dst, 0x11, 0x20);
                        var acc_NewPasswordHashed = Convert.ToBase64String(dst);

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
                return RedirectToAction("Index", "EditProfile", new { id = acc_Id });
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message.ToString());
                TempData["Exception"] = @"Swal.fire({ icon: 'error', title: 'Error !', text: `" + e.Message.Replace("\\", "/").Replace("`", "'") + @"`, showConfirmButton: true });";
                _logger.LogTrace("End edit profile.");
                return RedirectToAction("Index", "EditProfile");
            } // End try catch
        } // End EditProfile
    } // End EditProfileContrller
}