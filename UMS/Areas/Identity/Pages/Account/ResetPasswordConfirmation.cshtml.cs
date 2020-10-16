using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

/*
 * Name: ResetPasswordConfirmationModel.cs (Extend: PageModel)
 * Author: Idenity system
 * Description: Confirmations for reset password.
 */

namespace User_Management_System.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class ResetPasswordConfirmationModel : PageModel
    {
        private readonly ILogger<ResetPasswordConfirmationModel> _logger;
        /*
         * Name: ResetPasswordConfirmationModel
         * Parameter: logger(ILogger)
         */
        public ResetPasswordConfirmationModel(ILogger<ResetPasswordConfirmationModel> logger)
        {
            _logger = logger;
            _logger.LogDebug("Start password confirmation model.");
        } // End constructor

        public void OnGet() { _logger.LogTrace("Reset password confirmation on get."); } // End OnGet
    } // End ResetPasswordConfirmationModel
}