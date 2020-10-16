using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

/*
 * Name: ForgotPasswordConfirmation.cs (Extend: PageModel)
 * Author: Idenity system
 * Descriptions: Confirmation for forgot password.
 */

namespace User_Management_System.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class ForgotPasswordConfirmation : PageModel
    {
        private readonly ILogger<ForgotPasswordConfirmation> _logger;
        /*
         * Name: ForgotPasswordConfirmation
         * Parameter: logger(ILogger<ForgotPasswordConfirmation>)
         */
        public ForgotPasswordConfirmation(ILogger<ForgotPasswordConfirmation> logger)
        {
            _logger = logger;
            _logger.LogDebug("Start forgot password confirmation.");
        }  // End constructor

        public void OnGet() { _logger.LogTrace("Forgot forgot password confirmation on get."); } // End OnGet

        /*
         * Name: OnPost
         * Description: Direction to home page.
         */
        public IActionResult OnPost()
        {
            _logger.LogTrace("Forgot forgot password confirmation on post.");
            return RedirectToPage("Login");
        } // End OnPost
    } // End ForgotPasswordConfirmation
}