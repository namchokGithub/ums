using System;
using System.ComponentModel.DataAnnotations;
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
 * Name: ResetPasswordModel.cs
 * Namespace: UMS.Areas.Identity.Pages.Account
 * Author: Idenity system
 */

namespace UMS.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class ResetPasswordModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<ResetPasswordModel> _logger;

        public ResetPasswordModel(UserManager<ApplicationUser> userManager, ILogger<ResetPasswordModel> logger)
        {
            _logger = logger;
            _logger.LogDebug("Start Reset password model.");
            _userManager = userManager;
        } // End constructor

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required]
            [RegularExpression(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$"
                , ErrorMessage = "The Email is not valid.")]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required]
            [DataType(DataType.Password)]
            [StringLength(100
                , ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[$@$!%*?&])[A-Za-z0-9$@$!%*?&]+$"
                , ErrorMessage = "The password must contain at least <br> 1 uppercase, 1 lowercase, 1 digit and 1 special character.")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }

            public string Code { get; set; }
        }

        /*
         * Name: OnGet
         * Parameter: code(String)
         * Description: Check code for set password
         */
        public IActionResult OnGet(string code = null)
        {
            try
            {
                _logger.LogTrace("Start reset password on get.");
                if (code == null)
                {
                    _logger.LogError("A code must be supplied for password reset.");
                    throw new Exception("A code must be supplied for password reset.");
                }
                else
                {
                    Input = new InputModel
                    {
                        Code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code))
                    };
                    _logger.LogDebug("Encoding to input model.");
                    _logger.LogTrace("End reset password on get.");
                    return Page();
                }
            } catch (Exception e)
            {
                _logger.LogError(e.Message.ToString());
                string message = @"Swal.fire({ icon: 'error', title: 'Error !', text: `" + e.Message + @"`, showConfirmButton: true })";
                TempData["Exception"] = message;
                _logger.LogTrace("End reset password on get.");
                return Page();
            } // End try catch
        } // End OnGet

        /*
         * Name: OnPostAsync
         * Parameter: none
         * Description: Check code for set password
         */
        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                _logger.LogTrace("Start reset password on post.");
                if (!ModelState.IsValid)
                {
                    _logger.LogWarning("Model State is false.");
                    _logger.LogTrace("End reset password on post.");
                    return Page();
                }
                _logger.LogDebug("Finding by email.");
                var user = await _userManager.FindByEmailAsync(Input.Email);
                if (user == null)
                {
                    // Don't reveal that the user does not exist
                    _logger.LogWarning("User not found.");
                    _logger.LogTrace("End reset password on post.");
                    return RedirectToPage("./Login");
                }
                _logger.LogTrace("Resetign password.");
                var result = await _userManager.ResetPasswordAsync(user, Input.Code, Input.Password);

                if (result.Succeeded)
                {
                    _logger.LogInformation("User reseted password.");
                    _logger.LogTrace("End reset password on post.");
                    return RedirectToPage("./ResetPasswordConfirmation");
                }
                string errorStr = "";
                foreach (var error in result.Errors)
                {
                    errorStr += error.Description + " (" + error.Code + "). ";
                    ModelState.AddModelError(string.Empty, error.Description);
                } // End loop get error
                _logger.LogError(errorStr.ToString());
                TempData["Exception"] = @"Swal.fire({ icon: 'error', title: 'Error !', text: `" + errorStr + @"`, showConfirmButton: true })";
                _logger.LogTrace("End reset password on post.");
                return Page();
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message.ToString());
                string message = @"Swal.fire({ icon: 'error', title: 'Error !', text: `" + e.Message + @"`, showConfirmButton: true })";
                TempData["Exception"] = message;
                _logger.LogTrace("End reset password on post.");
                return Page();
            } // End try catch
        } // End OnPostAsync
    } // End ResetPasswordModel
}
