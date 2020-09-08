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
        }

        /*
         * Name: OnGet
         * Parameter: none
         * Description: 
         */
        public void OnGet()
        {
        } // End OnGet

        /*
         * Name: OnPost
         * Parameter: returnUrl(String)
         * Description: For log out user
         */
        public async Task<IActionResult> OnPost(string returnUrl = null)
        {
            await _signInManager.SignOutAsync();
            _logger.LogInformation("User logged out.");
            if (returnUrl != null)
            {
                return LocalRedirect(returnUrl);
            }
            else
            {
                return RedirectToPage();
            }
        } // End OnPost
    } // End LogoutModel
}
