using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Encodings.Web;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
//using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using UMS.Areas.Identity.Data;
using EmailService;
using System.Linq;

/*
 * Name: ForgotPasswordModel.cs
 * Namespace: UMS.Areas.Identity.Pages.Account
 * Author: Idenity system
 */

namespace UMS.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class ForgotPasswordModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IEmailSender _emailSender;

        public ForgotPasswordModel(UserManager<ApplicationUser> userManager, IEmailSender emailSender)
        {
            _userManager = userManager;
            _emailSender = emailSender;
        } // End contructor

        [BindProperty]
        public InputModel Input { get; set; } // For input value

        /*
         * Name: InputModel
         * Description: for input email and ser expession
         */
        public class InputModel
        {
            [Required]
            [RegularExpression(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$"
                , ErrorMessage = "The Email is not valid.")]
            public string Email { get; set; }
        } // End InputModel

        /*
         * Name: OnPostAsync
         * Parameter: none
         * Description: Send email for set password.
         */
        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var user = await _userManager.FindByEmailAsync(Input.Email); // Find user

                    if (user == null)
                    {
                        // Don't have this user
                        Console.WriteLine("Not found User!!");
                        return RedirectToPage("Login");
                    } // End check user is null

                    var code = await _userManager.GeneratePasswordResetTokenAsync(user); // Gen token for this user
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));

                    var callbackUrl = Url.Page(
                        "/Account/ResetPassword",
                        pageHandler: null,
                        values: new { area = "Identity", code },
                        protocol: Request.Scheme); // Craete call back url

                    var message = new Message(
                        new string[] { Input.Email },
                        "Reset Password",
                        @$"
                        <h2>Hello!, {Input.Email} </h2>
                        <br>
                        We got a request to reset your UMS password.
                        <br>
                        You can reset your password by clicking the link 
                        <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'asp-route-Email='{Input.Email}'>
                            <b>Reset password</b>
                        </a>.
                        <br>
                        If you ignore this message, your password won't be changed.
                        <br><br><br>
                        <hr>
                        Best regards,
                        <br>
                        User Management System
                      "
                        ); // End craete message 

                    await _emailSender.SendEmailAsync(message);

                    return RedirectToPage("./ForgotPasswordConfirmation");
                }

                return Page();
            } catch (Exception e)
            {
                // Set sweet alert with error messages
                string message = @"Swal.fire({ icon: 'error', title: 'Error !', text: `" + e.Message + @"`, showConfirmButton: true })";
                // Send alert to home pages
                TempData["Exception"] = message;
                return Page();
            } // End try catch
        } // End OnPostAsync
    } // End ForgotPasswordModel
}
