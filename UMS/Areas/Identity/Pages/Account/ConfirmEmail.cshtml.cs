using System;
using System.Text;
using System.Threading.Tasks;
using UMS.Areas.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

/*
 * Name: ConfirmEmailModel.cs (Extend :PageModel)
 * Namespace: UMS.Areas.Identity.Pages.Account
 * Author: Idenity system
 * Description: The confirmation email.
 */

namespace UMS.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class ConfirmEmailModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<ConfirmEmailModel> _logger;
        /*
         * Name: ConfirmEmailModel
         * Parameter: userManager(UserManager<ApplicationUser>), logger(ILogger<ConfirmEmailModel>)
         */
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
         * Description: Checking code and user before confirm email.
         */
        public async Task<IActionResult> OnGetAsync(string userId, string code)
        {
            try
            {
                _logger.LogTrace("Start confirm email on get.");
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
                    _logger.LogError("Error confirming your email.");
                }
                _logger.LogTrace("End confirm email on get.");
                return Page();
            } catch (Exception e)
            {
                _logger.LogError(e.Message.ToString());
                TempData["Exception"] = @"Swal.fire({ icon: 'error', title: 'Error !', text: `" + e.Message.Replace("\\", "/") + @"`, showConfirmButton: true });";
                _logger.LogTrace("End confirm email on get.");
                return Page();
            } // End try catch
        } // End OnGetAsync
    }// End ConfirmEmailModel
}