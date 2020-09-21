﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Encodings.Web;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using UMS.Areas.Identity.Data;
using EmailService;
using System.Linq;
using Microsoft.Extensions.Logging;
using UMS.Models;
using Microsoft.EntityFrameworkCore;

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
        private readonly EditProfileContext _editprofileContext;
        private readonly IEmailSender _emailSender;
        private readonly ILogger<ForgotPasswordModel> _logger;

        public ForgotPasswordModel(UserManager<ApplicationUser> userManager,
            EditProfileContext editprofileContext, 
            IEmailSender emailSender, 
            ILogger<ForgotPasswordModel> logger)
        {
            _userManager = userManager;
            _editprofileContext = editprofileContext;
            _emailSender = emailSender;
            _logger = logger;
            _logger.LogDebug("Start Forgot Password model.");
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
                _logger.LogTrace("Start forgot password on post.");
                if (ModelState.IsValid)
                {
                    var user = await _userManager.FindByEmailAsync(Input.Email); // Find user
                    _logger.LogDebug("Finding user.");
                    if (user == null)
                    {
                        _logger.LogWarning("The user was not found.");
                        TempData["Exception"] = @"Swal.fire({ icon: 'error', title: 'Error !', text: 'The user was not found.'})";
                        return Page();
                    } // End check user is null

                    string sqltext = $"EXEC [dbo].ums_Get_user '{user.Id}'";
                    // Query data from "dbo.Account" and Convert to List<EditAccount>
                    var us = _editprofileContext.EditProfile.FromSqlRaw(sqltext).ToList().FirstOrDefault<EditProfile>();
                    if (us.acc_TypeAccoutname.ToString().ToLower() != "Email".ToLower())
                    {
                        _logger.LogWarning("This user can not change password (Social Account).");
                        TempData["Exception"] = @"Swal.fire({ icon: 'warning', title: 'Can not change password!', text: 'This email is login with social media'})"; // เป็น Social media ไม่สามารถเปลี่ยน Password ได้
                        return Page();
                    }
                    _logger.LogDebug("Generating code.");
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
                            You can change your password by clicking the link 
                            <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'asp-route-Email='{Input.Email}'>
                                <b>Change password</b>
                            </a>.
                            <br>
                            If you ignore this message, your password won't be changed.
                            <br><br><br>
                            <hr>
                            Best regards,
                            <br>
                            User Management System.
                          "
                        ); // End craete message 
                    _logger.LogTrace("Sending email.");
                    await _emailSender.SendEmailAsync(message);
                    _logger.LogTrace("End forgot password on post.");
                    return RedirectToPage("./ForgotPasswordConfirmation");
                }
                _logger.LogTrace("End forgot password on post.");
                return Page();
            } catch (Exception e)
            {
                _logger.LogError(e.Message.ToString());
                string message = @"Swal.fire({ icon: 'error', title: 'Error !', text: `" + e.Message + @"`, showConfirmButton: true })";
                TempData["Exception"] = message;
                _logger.LogTrace("End forgot password on post.");
                return Page();
            } // End try catch
        } // End OnPostAsync
    } // End ForgotPasswordModel
}
