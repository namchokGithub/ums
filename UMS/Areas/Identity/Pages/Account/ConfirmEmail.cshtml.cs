using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using UMS.Areas.Identity.Data;

/*
 * Name: ConfirmEmailModel.cs
 * Namespace: UMS.Areas.Identity.Pages.Account
 * Author: Idenity system
 * Description: Confirmation of email
 */

namespace UMS.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class ConfirmEmailModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<ConfirmEmailModel> _logger;
        public ConfirmEmailModel(UserManager<ApplicationUser> userManager, ILogger<ConfirmEmailModel> logger)
        {
            _userManager = userManager;
            _logger = logger;
            _logger.LogDebug("Start Confirm Email model.");
        } // End constructor

        [TempData]
        public string StatusMessage { get; set; }

        /*
         * Name: OnGetAsync
         * Parameter: userId(string), code(String)
         * Description: Check code and user before confirm email
         */
        public async Task<IActionResult> OnGetAsync(string userId, string code)
        {
            try
            {
                _logger.LogTrace("Start confirm email On get.");
                if (userId == null || code == null)
                {
                    _logger.LogWarning("User Id or code is null.");
                    return RedirectToPage("/Index");
                }

                _logger.LogTrace("Finding user.");
                var user = await _userManager.FindByIdAsync(userId);
                if (user == null)
                {
                    _logger.LogWarning("User is null.");
                    return NotFound($"Unable to load user with ID '{userId}'.");
                }
                _logger.LogDebug("Generating code for comfirm.");
                code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code));
                _logger.LogDebug("Confirming email.");
                var result = await _userManager.ConfirmEmailAsync(user, code);
                if(result.Succeeded)
                {
                    StatusMessage = "Thank you for confirming your email.";
                    _logger.LogTrace("Thank you for confirming your email.");
                } else
                {
                    StatusMessage = "Error confirming your email.";
                    _logger.LogTrace("Error confirming your email.");
                }
                _logger.LogTrace("End confirm email On get.");
                return Page();
            } catch (Exception e)
            {
                _logger.LogError(e.Message.ToString());
                // Set sweet alert with error messages
                string message = @"Swal.fire({ icon: 'error', title: 'Error !', text: `" + e.Message + @"`, showConfirmButton: true })";
                // Send alert to home pages
                TempData["Exception"] = message;
                _logger.LogTrace("End confirm email On get.");
                return Page();
            } // End try catch
        } // End OnGetAsync
    }// End ConfirmEmailModel
}
