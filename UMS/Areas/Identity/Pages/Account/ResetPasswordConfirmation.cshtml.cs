﻿using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

/*
 * Name: ResetPasswordConfirmationModel.cs (Extend: PageModel)
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

        public void OnGet() { _logger.LogTrace("Reset Password Confirmation on get."); } // End OnGet
    } // End ResetPasswordConfirmationModel
}
