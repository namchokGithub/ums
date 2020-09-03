﻿using System;
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
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required]
            [RegularExpression(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$"
                , ErrorMessage = "The Email is not valid.")]
            public string Email { get; set; }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(Input.Email);

                if (user == null)
                {
                    // Don't have this user
                    Console.WriteLine("Not found User!!");
                    return RedirectToPage("Login");
                }

                var code = await _userManager.GeneratePasswordResetTokenAsync(user);
                code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                var callbackUrl = Url.Page(
                    "/Account/ResetPassword",
                    pageHandler: null,
                    values: new { area = "Identity", code },
                    protocol: Request.Scheme);
                
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
                );
                await _emailSender.SendEmailAsync(message);

                return RedirectToPage("./ForgotPasswordConfirmation");
            }

            return Page();
        }
    }
}
