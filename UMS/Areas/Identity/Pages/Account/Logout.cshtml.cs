using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;
using User_Management_System.Areas.Identity.Data;

/*
 * Name: LogoutModel.cs (Extend: PageModel)
 * Namespace: UMS.Areas.Identity.Pages.Account
 * Author: Idenity system
 */

namespace User_Management_System.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class LogoutModel : PageModel
    {
        private readonly ILogger<LogoutModel> _logger;
        private readonly SignInManager<ApplicationUser> _signInManager;
        /*
         * Name: LogoutModel 
         * Parameter: signInManager (SignInManager<ApplicationUser>), logger(ILogger<LogoutModel>)
         */
        public LogoutModel(SignInManager<ApplicationUser> signInManager, ILogger<LogoutModel> logger)
        {
            _signInManager = signInManager;
            _logger = logger;
            _logger.LogDebug("Start logout.");
        } // End constructor

        /*
         * Name: OnGet
         * Description: Deleting a cookie for user.
         */
        public async void OnGet()
        {
            try
            {
                _logger.LogTrace("Start logout on get.");
                ViewData["URL"] = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";

                await _signInManager.SignOutAsync();
                _logger.LogInformation("User logged out.");
                foreach (var cookie in HttpContext.Request.Cookies) { Response.Cookies.Delete(cookie.Key); }
                _logger.LogTrace("The destruction of cookies AspNetCore Identity Application.");

                _logger.LogTrace("End logout model on get.");
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message.ToString());
                TempData["Exception"] = @"Swal.fire({ icon: 'error', title: 'Error !', text: `" + e.Message.Replace("`", "'").Replace("\\", "/") + @"`, showConfirmButton: true });";
                _logger.LogTrace("End logout model on get.");
            }
        } // End OnGet

        /*
         * Name: OnPost
         * Description: The logout of system.
         */
        public async Task<IActionResult> OnPost()
        {
            _logger.LogTrace("Start Logout on post.");
            ViewData["URL"] = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";

            await _signInManager.SignOutAsync();
            _logger.LogInformation("User logged out.");
            foreach (var cookie in HttpContext.Request.Cookies) { Response.Cookies.Delete(cookie.Key); }
            _logger.LogTrace("The destruction of cookies AspNetCore Identity Application.");

            _logger.LogTrace("End Log out model On Post.");
            return Redirect($"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}/Identity/Account/Logout");
        } // End OnPost
    } // End LogoutModel
}