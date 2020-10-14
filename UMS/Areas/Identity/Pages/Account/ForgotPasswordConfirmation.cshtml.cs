using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

/*
 * Name: ForgotPasswordConfirmation.cs
 * Namespace: UMS.Areas.Identity.Pages.Account
 * Author: Idenity system
 */

namespace UMS.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class ForgotPasswordConfirmation : PageModel
    {
        private readonly ILogger<ForgotPasswordConfirmation> _logger;

        public ForgotPasswordConfirmation(ILogger<ForgotPasswordConfirmation> logger)
        {
            _logger = logger;
            _logger.LogDebug("Start Forgot Password Confirmation.");
        }  // End constructor

        /*
         * Name: OnGet
         * Parameter: none
         */
        public void OnGet() { _logger.LogTrace("Forgot Password Confirmation On get."); } // End OnGet

        /*
         * Name: OnPost
         * Parameter: none
         * Description: return to home page
         */
        public IActionResult OnPost()
        {
            _logger.LogTrace("Forgot Password Confirmation On post.");
            return RedirectToPage("Login");
        } // End OnPost
    } // End ForgotPasswordConfirmation
}
