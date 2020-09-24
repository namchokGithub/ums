using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using UMS.Areas.Identity.Data;

/*
 * Name: LogoutModel.cs
 * Namespace: UMS.Areas.Identity.Pages.Account
 * Author: Idenity system
 */

namespace UMS.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class LogoutModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger<LogoutModel> _logger;

        public LogoutModel(SignInManager<ApplicationUser> signInManager, ILogger<LogoutModel> logger)
        {
            _signInManager = signInManager;
            _logger = logger;
            _logger.LogDebug("Starting Logout model.");
        } // End constructor

        /*
         * Name: OnGet
         * Parameter: none
         * Description: 
         */
        public void OnGet() { _logger.LogTrace("Logout model On get."); } // End OnGet

        /*
         * Name: OnPost
         * Parameter: returnUrl(String)
         * Description: For log out user
         */
        public async Task<IActionResult> OnPost()
        {
            _logger.LogTrace("Start Logout model On Post.");
            _logger.LogTrace("Signing out.");
            await _signInManager.SignOutAsync();
            _logger.LogInformation("User logged out.");
            Response.Cookies.Delete(".AspNetCore.Identity.Application");
            _logger.LogTrace("Clear cookies AspNetCore Identity Application.");
            _logger.LogTrace("End Log out model On Post.");
            return RedirectToPage("./Login");
        } // End OnPost
    } // End LogoutModel
}
