using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

/*
 * Name: ResetPasswordConfirmationModel.cs
 * Namespace: UMS.Areas.Identity.Pages.Account
 * Author: Idenity system
 */

namespace UMS.Areas.Identity.Pages.Account
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
            _logger.LogDebug("Start Reset Password Confirmation model.");
        } // End constructor

        /*
         * Name: OnGet
         * Parameter: none
         */
        public void OnGet()
        {
            _logger.LogTrace("Reset Password Confirmation on get.");
        } // End OnGet
    } // End ResetPasswordConfirmationModel
}
