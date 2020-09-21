using System;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using UMS.Areas.Identity.Data;
using UMS.Models;

/*
 * Name: EditProfileController.cs
 * Namespace: Controllers
 * Author: Wannapa Srijermtong
 */

namespace UMS.Controllers
{
    public class EditProfileController : Controller
    {
        // For context of database
        private readonly EditProfileContext _editprofileContext;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger<EditProfileController> _logger;

        /*
         * Name: EditProfileController
         * Parameter: editprofileContext(EditProfileContext), signInManager(SignInManager<ApplicationUser>)
         * Author: Wannapa Srijermtong
         * Description: Set context for database
         */
        public EditProfileController(EditProfileContext editprofileContext, SignInManager<ApplicationUser> signInManager, ILogger<EditProfileController> logger)
        {
            try
            {
                _logger = logger;
                _logger.LogTrace("NLog injected into EditProfileController.");
                _editprofileContext = editprofileContext;
                _logger.LogTrace("Edit Profile Context injected into EditProfileController.");
                _signInManager = signInManager;
                _logger.LogTrace("Sign In Manager injected into EditProfileController.");
                _logger.LogTrace("Start EditProfile Controller Constructor.");
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message.ToString());
                _logger.LogTrace("End EditProfileController Constructor.");
            }// End try catch
        } // End Constructor

        /*
         * Name: Index
         * Parameter: Id
         * Author: Wannapa Srijermtong
         * Description: Get Firstname, Lastname and LoginProvider by UserId
         */
        public IActionResult Index()
        {
            try
            {
                _logger.LogTrace("Start Index");
                var UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                ViewData["UserId"] = UserId ?? throw new Exception("The user ID not found !.");
                string sqltext = $"EXEC [dbo].ums_Get_user '{UserId}'";
                // Query data from "dbo.EditProfile" and Convert to List<EditProfile>
                var user = _editprofileContext.EditProfile.FromSqlRaw(sqltext).ToList<EditProfile>().FirstOrDefault();
                _logger.LogDebug("Get user by ID.");
                ViewData["User"] = user ?? throw new Exception("Calling a method on a null object reference.");
                _logger.LogTrace("End Index");
                return View();
            }
            catch (Exception e)
            {
                TempData["EditProfileException"] = @"Swal.fire({ icon: 'error', title: 'Error !', text: `" + e.Message + @"`, showConfirmButton: true })";
                _logger.LogTrace("End Index.");
                return View();
            } // End try catch
        } // End Index

        /*
         * Name: editProfile
         * Parameter: none
         * Author: Wannapa Srijermtong
         * Description: Edit user profile
         */
        [HttpPost]
        public async Task<IActionResult> editProfile()
        {
            try
            {
                _logger.LogTrace("Start Edit Profile.");
                _logger.LogDebug("Getting value from httpcontext request.");
                
                //Get data from Form Input
                var acc_Id = HttpContext.Request.Form["acc_Id"];
                if(acc_Id.ToString() == null || acc_Id.ToString() == "") throw new Exception("Calling a method on a null object reference.");
                var acc_Firstname = HttpContext.Request.Form["acc_Firstname"];
                var acc_Lastname = HttpContext.Request.Form["acc_Lastname"];
                var acc_CurrentPassword = HttpContext.Request.Form["acc_CurrentPassword"];
                var acc_NewPassword = HttpContext.Request.Form["acc_NewPassword"];
                var acc_ConfirmPassword = HttpContext.Request.Form["acc_ConfirmPassword"];
                _logger.LogTrace("Check regular expression.");
               
                // Regular expression
                var RegExName = @"^[a-zA-Z]+(([a-zA-Z])?[a-zA-Z]*)*$";
                var RegExPassword = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[\[\]\\\|\/\.\,\'\""\()\{}\<>_#$!%@@^฿*?&\-\+\=])[A-Za-z0-9\[\]\\\|\/\.\,\'\""\()\{}\<>_#$!%@@^฿*?&\-\+\=]+$";
                // Validation if acc_Firstname do not math with Regular expression.
                if (!Regex.IsMatch(acc_Firstname, RegExName) && acc_Firstname != "")
                {
                    TempData["EditProfileErrorResult"] = @"toastr.warning('The First name can not be blank and must only character.')";
                    _logger.LogTrace("End Edit Profile.");
                    return RedirectToAction("Index", "EditProfile", new { id = acc_Id });
                }
                // Validation if acc_Lastname do not math with Regular expression.
                if (!Regex.IsMatch(acc_Lastname, RegExName) && acc_Lastname != "")
                {
                    TempData["EditProfileErrorResult"] = @"toastr.warning('The Last name can not be blank and must only character.')";
                    _logger.LogTrace("End Edit Profile.");
                    return RedirectToAction("Index", "EditProfile", new { id = acc_Id });
                }
                // Validation if acc_CurrentPassword do not math with Regular expression.
                if (!Regex.IsMatch(acc_CurrentPassword, RegExPassword) && acc_CurrentPassword != "")
                {
                    TempData["EditProfileErrorResult"] = @"toastr.warning('The password must contain at least <br> 1 uppercase, 1 lowercase, 1 digit and 1 special character.')";
                    _logger.LogTrace("End Edit Profile.");
                    return RedirectToAction("Index", "EditProfile", new { id = acc_Id });
                }
                // Validation if acc_NewPassword do not math with Regular expression.
                if (!Regex.IsMatch(acc_NewPassword, RegExPassword) && acc_NewPassword != "")
                {
                    TempData["EditProfileErrorResult"] = @"toastr.warning('The password must contain at least <br> 1 uppercase, 1 lowercase, 1 digit and 1 special character.')";
                    _logger.LogTrace("End Edit Profile.");
                    return RedirectToAction("Index", "EditProfile", new { id = acc_Id });
                }
                // Validation if acc_ConfirmPassword do not math with Regular expression.
                if (!Regex.IsMatch(acc_ConfirmPassword, RegExPassword) && acc_ConfirmPassword != "")
                {
                    TempData["EditProfileErrorResult"] = @"toastr.warning('The password must contain at least <br> 1 uppercase, 1 lowercase, 1 digit and 1 special character.')";
                    _logger.LogTrace("End Edit Profile.");
                    return RedirectToAction("Index", "EditProfile", new { id = acc_Id });
                }

                // Validation if acc_Firstname, acc_Lastname, acc_CurrentPassword, acc_NewPassword and acc_ConfirmPassword is blank.
                if (acc_Firstname == "" || acc_Lastname == "" || acc_CurrentPassword == "" || acc_NewPassword == "" || acc_ConfirmPassword == "")
                {
                    // Validation if acc_Firstname and acc_Lastname is not blank.
                    if (acc_Firstname != "" && acc_Lastname != "")
                    {
                        // SQL text for execute procedure
                        string sqlUpdateUser = $"ums_Update_user '{acc_Id}', '{acc_Firstname}', '{acc_Lastname}'";
                        _editprofileContext.Database.ExecuteSqlRaw(sqlUpdateUser);
                        _logger.LogDebug("Update name's user.");
                        var resultUpdate_user = false;
                        while (!resultUpdate_user)
                        {
                            try
                            {
                                _editprofileContext.SaveChanges();
                                _logger.LogDebug("Save changes: Check update user.");
                                _logger.LogTrace("Query succeeded.");
                                TempData["EditProfileSuccessResult"] = @"toastr.success('Edit profile successfully!')";
                                resultUpdate_user = true; // If success
                            }
                            catch (Exception e)
                            {
                                _logger.LogError(e.Message.ToString());
                                TempData["Exception"] = @"Swal.fire({ icon: 'error', title: 'Error !', text: `" + e.Message + @"`, showConfirmButton: true })";
                            } // End try catch
                        } // Check if succeeded

                        _logger.LogTrace("End Edit Profile.");
                        return RedirectToAction("Index", "EditProfile", new { id = acc_Id });
                    }
                    // Toastr if Edit profile blank.
                    TempData["EditProfileErrorResult"] = @"toastr.warning('The input can not be blank!')";
                    _logger.LogTrace("End Edit Profile.");
                    return RedirectToAction("Index", "EditProfile", new { id = acc_Id });
                } // End validate all input
                // Validation if acc_CurrentPassword is not blank.
                if (acc_CurrentPassword != "")
                {
                    // Validation if acc_NewPassword and acc_ConfirmPassword do not match.
                    if (acc_NewPassword != acc_ConfirmPassword)
                    {
                        TempData["EditProfileErrorResult"] = @"toastr.warning('The new password and confirmation password do not match.')";
                        _logger.LogTrace("End Edit Profile.");
                        return RedirectToAction("Index", "EditProfile", new { id = acc_Id });
                    }
                    var result = await _signInManager.PasswordSignInAsync(User.Identity.Name, acc_CurrentPassword, false, lockoutOnFailure: false);
                    _logger.LogDebug("Signing in with password.");
                    if (!result.Succeeded)
                    {
                        TempData["EditProfileErrorResult"] = @"toastr.warning('Current password is not correct!')";
                        _logger.LogDebug("Current password is not correct!. Cannot sign in.");
                        _logger.LogTrace("End Edit Profile.");
                        return RedirectToAction("Index", "EditProfile", new { id = acc_Id });
                    }
                    else
                    {
                        if (acc_NewPassword == "") throw new Exception("New password is null.");

                        _logger.LogDebug("Hasing password.");
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
                        string sqlUpdateAll = $"ums_Update_all '{acc_Id}', '{acc_Firstname}', '{acc_Lastname}', '{acc_NewPasswordHashed}'";
                        _editprofileContext.Database.ExecuteSqlRaw(sqlUpdateAll);
                        _logger.LogDebug("Update name's user and password.");
                        var resultUpdate_all = false;
                        while (!resultUpdate_all)
                        {
                            try
                            {
                                _editprofileContext.SaveChanges();
                                _logger.LogDebug("Save changes: Check update user.");
                                _logger.LogTrace("Query succeeded.");
                                TempData["EditProfileSuccessResult"] = @"toastr.success('Edit profile successfully!')";
                                resultUpdate_all = true; // If success
                            }
                            catch (Exception e)
                            {
                                _logger.LogError(e.Message.ToString());
                                TempData["Exception"] = @"Swal.fire({ icon: 'error', title: 'Error !', text: `" + e.Message + @"`, showConfirmButton: true })";
                            } // End try catch
                        } // Check if succeeded
                    } // End if current password is correct
                }
                else
                {
                    // SQL text for execute procedure
                    string sqlUpdateUser = $"ums_Update_user '{acc_Id}', '{acc_Firstname}', '{acc_Lastname}'";
                    _editprofileContext.Database.ExecuteSqlRaw(sqlUpdateUser);
                    _logger.LogDebug("Update name's user.");
                    var result = false;
                    while (!result)
                    {
                        try
                        {
                            _editprofileContext.SaveChanges();
                            _logger.LogDebug("Save changes: Check update user.");
                            _logger.LogTrace("Query succeeded.");
                            TempData["EditProfileSuccessResult"] = @"toastr.success('Edit profile successfully!')";
                            result = true; // If success
                        }
                        catch (Exception e)
                        {
                            _logger.LogError(e.Message.ToString());
                            TempData["Exception"] = @"Swal.fire({ icon: 'error', title: 'Error !', text: `" + e.Message + @"`, showConfirmButton: true })";
                        } // End try catch
                    } // Check if check succeeded

                } // If current password is not blank
                _logger.LogTrace("End Edit Profile.");
                return RedirectToAction("Index", "EditProfile", new { id = acc_Id });
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message.ToString());
                TempData["Exception"] = @"Swal.fire({ icon: 'error', title: 'Error !', text: `" + e.Message + @"`, showConfirmButton: true })";
                _logger.LogTrace("End Edit Profile.");
                return RedirectToAction("Index", "EditProfile");
            } // End try catch
        } // End editProfile
    } // End EditProfileContrller
}