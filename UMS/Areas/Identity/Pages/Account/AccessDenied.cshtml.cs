using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc.RazorPages;

/*
 * Name: AccessDeniedModel.cs (extend: PageModel)
 * Author: Idenity system
 * Description: The page for access to denied sites.
 */

namespace User_Management_System.Areas.Identity.Pages.Account
{
    public class AccessDeniedModel : PageModel
    {
        private readonly ILogger<AccessDeniedModel> _logger;
        /*
         * Name: AccessDeniedModel
         * Parameter: logger(ILogger<AccessDeniedModel>)
         */
        public AccessDeniedModel(ILogger<AccessDeniedModel> logger)
        {
            _logger = logger;
            _logger.LogDebug("Start access denined model.");
        } // End constructor

        public void OnGet() { _logger.LogTrace("Access denined on get."); } // End OnGet

        /*
         * Name: OnPost
         * Description: The destruction of system cookie.
         */
        public IActionResult OnPost()
        {
            try
            {
                _logger.LogTrace("Access denined on post.");
                foreach (var cookie in HttpContext.Request.Cookies) { Response.Cookies.Delete(cookie.Key); }
                _logger.LogTrace("Clear cookies AspNetCore Identity Application.");
                return RedirectToPage("/Login");
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message.ToString());
                TempData["Exception"] = @"Swal.fire({ icon: 'error', title: 'Error !', text: `" + e.Message.Replace("`", "'").Replace("\\", "/") + @"`, showConfirmButton: true });";
                _logger.LogTrace("End access denined on post.");
                return Page();
            } // End try catch
        } // End OnPost
    } // End AccessDeniedModel
}