using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        /*
         * Name: EditProfileController
         * Parameter: editprofileContext(EditProfileContext), signInManager(SignInManager<ApplicationUser>)
         * Author: Wannapa Srijermtong
         * Description: Set context for database
         */
        public EditProfileController(EditProfileContext editprofileContext, SignInManager<ApplicationUser> signInManager)
        {
            _editprofileContext = editprofileContext;
            _signInManager = signInManager;
        }

        /*
         * Name: Index
         * Parameter: Id
         * Author: Wannapa Srijermtong
         * Description: Show Firstname and Lastname of user
         */
        public IActionResult Index(string Id)
        {
            // SQL text for exextut procedure
            string sqltext = $"EXEC [dbo].ums_Get_user '{Id}'";

            // Query data from "dbo.EditProfile" and Convert to List<EditProfile>
            var user = _editprofileContext.EditProfile.FromSqlRaw(sqltext).ToList<EditProfile>().FirstOrDefault();

            // Send data to view Index.cshtml
            ViewData["User"] = user;
            return View();
        }

        /*
         * Name: editProfile
         * Parameter: none
         * Author: Wannapa Srijermtong
         * Description: Edit user profile
         */
        [HttpPost]
        public async Task<IActionResult> editProfile()
        {
            //Get data from Form Input
            var acc_Id = HttpContext.Request.Form["acc_Id"];
            var acc_Firstname = HttpContext.Request.Form["acc_Firstname"];
            var acc_Lastname = HttpContext.Request.Form["acc_Lastname"];
            var acc_OldPassword = HttpContext.Request.Form["acc_OldPassword"];
            var acc_NewPassword = HttpContext.Request.Form["acc_NewPassword"];
            var acc_ConfirmPassword = HttpContext.Request.Form["acc_ConfirmPassword"];

            //Regular expression
            var RegExName = @"^[a-zA-Z]+(([a-zA-Z])?[a-zA-Z]*)*$";
            var RegExPassword = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[$!%*?&])[A-Za-z0-9$!%*?&]+$";

            // Validation if acc_Firstname do not math with Regular expression.
            if (!Regex.IsMatch(acc_Firstname, RegExName))
            {
                // Toastr if Edit profile blank.
                TempData["LoginErrorResult"] = @"toastr.warning('The Firstname must only character.')";

                return RedirectToAction("Index", "EditProfile", new { id = acc_Id });
            }

            // Validation if acc_Lastname do not math with Regular expression.
            if (!Regex.IsMatch(acc_Lastname, RegExName))
            {
                // Toastr if Edit profile blank.
                TempData["LoginErrorResult"] = @"toastr.warning('The Lastname must only character.')";

                return RedirectToAction("Index", "EditProfile", new { id = acc_Id });
            }

            // Validation if acc_OldPassword do not math with Regular expression.
            if (!Regex.IsMatch(acc_OldPassword, RegExPassword))
            {
                // Toastr if Edit profile blank.
                TempData["LoginErrorResult"] = @"toastr.warning('The password must contain at least <br> 1 uppercase, 1 lowercase, 1 digit and 1 special character.')";

                return RedirectToAction("Index", "EditProfile", new { id = acc_Id });
            }

            // Validation if acc_NewPassword do not math with Regular expression.
            if (!Regex.IsMatch(acc_NewPassword, RegExPassword))
            {
                // Toastr if Edit profile blank.
                TempData["LoginErrorResult"] = @"toastr.warning('The password must contain at least <br> 1 uppercase, 1 lowercase, 1 digit and 1 special character.')";

                return RedirectToAction("Index", "EditProfile", new { id = acc_Id });
            }

            // Validation if acc_ConfirmPassword do not math with Regular expression.
            if (!Regex.IsMatch(acc_ConfirmPassword, RegExPassword))
            {
                // Toastr if Edit profile blank.
                TempData["LoginErrorResult"] = @"toastr.warning('The password must contain at least <br> 1 uppercase, 1 lowercase, 1 digit and 1 special character.')";

                return RedirectToAction("Index", "EditProfile", new { id = acc_Id });
            }

            // Validation if acc_Firstname, acc_Lastname, acc_OldPassword, acc_NewPassword and acc_ConfirmPassword is blank.
            if (acc_Firstname == "" || acc_Lastname == "" || acc_OldPassword == "" || acc_NewPassword == "" || acc_ConfirmPassword == "")
            {
                // Validation if acc_Firstname and acc_Lastname is not blank.
                if(acc_Firstname != "" && acc_Lastname != "")
                {
                    TempData["LoginSuccessResult"] = @"toastr.success('Edit profile successfully!')";

                    // SQL text for execute procedure
                    string sqlUpdateUser = $"ums_Update_user '{acc_Id}', '{acc_Firstname}', '{acc_Lastname}'";

                    // Update Account
                    _editprofileContext.Database.ExecuteSqlRaw(sqlUpdateUser);

                    return RedirectToAction("Index", "EditProfile", new { id = acc_Id });
                }
                // Toastr if Edit profile blank.
                TempData["LoginErrorResult"] = @"toastr.warning('The input can not be blank!')";

                return RedirectToAction("Index", "EditProfile", new { id = acc_Id });
            }

            // Validation if acc_OldPassword is not blank.
            if (acc_OldPassword != "")
            {
                var result = await _signInManager.PasswordSignInAsync(User.Identity.Name, acc_OldPassword, false, lockoutOnFailure: false);

                if (!result.Succeeded)
                {
                    // Toastr if old password is not match with password hash in database.
                    TempData["LoginErrorResult"] = @"toastr.warning('Old password is not correct!')";

                    return RedirectToAction("Index", "EditProfile", new { id = acc_Id });
                }
                else
                {
                    // Change acc_NewPassword to Password Hash
                    byte[] salt;
                    byte[] buffer2;
                    if (acc_NewPassword == "")
                    {
                        throw new ArgumentNullException("password");
                    }
                    using (Rfc2898DeriveBytes bytes = new Rfc2898DeriveBytes(acc_NewPassword, 0x10, 0x3e8))
                    {
                        salt = bytes.Salt;
                        buffer2 = bytes.GetBytes(0x20);
                    }
                    byte[] dst = new byte[0x31];
                    Buffer.BlockCopy(salt, 0, dst, 1, 0x10);
                    Buffer.BlockCopy(buffer2, 0, dst, 0x11, 0x20);
                    var hashed = Convert.ToBase64String(dst);

                    // Toastr if Edit profile success.
                    TempData["LoginSuccessResult"] = @"toastr.success('Edit profile successfully!')";

                    // SQL text for execute procedure
                    string sqlUpdateAll = $"ums_Update_all '{acc_Id}', '{acc_Firstname}', '{acc_Lastname}', '{hashed}'";

                    // Update Account
                    _editprofileContext.Database.ExecuteSqlRaw(sqlUpdateAll);
                }
            }
            else
            {
                // Toastr if Edit profile success.
                TempData["LoginSuccessResult"] = @"toastr.success('Edit profile successfully!')";

                // SQL text for execute procedure
                string sqlUpdateUser = $"ums_Update_user '{acc_Id}', '{acc_Firstname}', '{acc_Lastname}'";
                
                // Update Account
                _editprofileContext.Database.ExecuteSqlRaw(sqlUpdateUser);
            }

            return RedirectToAction("Index", "EditProfile", new {id = acc_Id});
        }
    }
}