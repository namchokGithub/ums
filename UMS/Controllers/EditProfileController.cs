using System;
using System.Linq;
using System.Security.Cryptography;
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
         * Parameter: editprofileContext(EditProfileContext)
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
            string sqltext = $"EXEC [dbo].get_User '{Id}'";

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
            var acc_Id = HttpContext.Request.Form["acc_Id"];
            var acc_Firstname = HttpContext.Request.Form["acc_Firstname"];
            var acc_Lastname = HttpContext.Request.Form["acc_Lastname"];
            var acc_OldPassword = HttpContext.Request.Form["acc_OldPassword"];
            var acc_NewPassword = HttpContext.Request.Form["acc_NewPassword"];

            if (acc_OldPassword != "")
            {
                var result = await _signInManager.PasswordSignInAsync(User.Identity.Name, acc_OldPassword, false, lockoutOnFailure: false);

                if (!result.Succeeded)
                {
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

                    // SQL text for execute procedure
                    string sqlUpdateAll = $"update_All '{acc_Id}', '{acc_Firstname}', '{acc_Lastname}', '{hashed}'";

                    // Update Account
                    _editprofileContext.Database.ExecuteSqlRaw(sqlUpdateAll);
                }
            }
            else
            {
                // SQL text for execute procedure
                string sqlUpdateUser = $"update_User '{acc_Id}', '{acc_Firstname}', '{acc_Lastname}'";
                
                // Update Account
                _editprofileContext.Database.ExecuteSqlRaw(sqlUpdateUser);
            }

            return RedirectToAction("Index", "EditProfile", new {id = acc_Id});
        }
    }
}
