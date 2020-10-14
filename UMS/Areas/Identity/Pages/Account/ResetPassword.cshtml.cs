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
using System.ComponentModel.DataAnnotations;

/*
 * Name: ResetPasswordModel.cs (Extend : PageModel)
 * Namespace: UMS.Areas.Identity.Pages.Account
 * Author: Idenity system.
 */

namespace UMS.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class ResetPasswordModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<ResetPasswordModel> _logger;
        /*
         * Name: ResetPasswordModel
         * Parameter: userManager(UserManager<ApplicationUser>), logger(ILogger<ResetPasswordModel>)
         */
        public ResetPasswordModel(UserManager<ApplicationUser> userManager, ILogger<ResetPasswordModel> logger)
        {
            _logger = logger;
            _logger.LogDebug("Start reset password model.");
            _userManager = userManager;
        } // End constructor

        [BindProperty]
        public InputModel Input { get; set; }
        /*
         * Name: InputModel
         * Description: The recording of input.
         */
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
            [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[\:\;\[\]\\\|\/\.\,\'\""\()\{}\<>_#$!%@@^฿*?&\-\+\=])[A-Za-z0-9\:\;\[\]\\\|\/\.\,\'\""\()\{}\<>_#$!%@@^฿*?&\-\+\=]+$"
                , ErrorMessage = "The password must contain at least <br> 1 uppercase, 1 lowercase, 1 digit and 1 special character.")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }

            public string Code { get; set; }
        } // End input model

        /*
         * Name: OnGet
         * Parameter: code(String)
         * Description: To check the password setting code.
         */
        public IActionResult OnGet(string code = null)
        {
            try
            {
                _logger.LogTrace("Start reset password on get.");
                if (code == null)
                {
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
                TempData["Exception"] = @"Swal.fire({ icon: 'error', title: 'Error !', text: `" + e.Message.Replace("\\", "/") + @"`, showConfirmButton: true });";
                _logger.LogTrace("End reset password on get.");
                return Page();
            } // End try catch
        } // End OnGet

        /*
         * Name: OnPostAsync
         * Parameter: none
         * Description: Resetting a password.
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
                _logger.LogDebug("Finding user by email.");
                var user = await _userManager.FindByEmailAsync(Input.Email);
                if (user == null)
                {
                    // Don't reveal that the user does not exist
                    _logger.LogWarning("User not found.");
                    _logger.LogTrace("End reset password on post.");
                    return RedirectToPage("./Login");
                }
                _logger.LogDebug("Resettinng password.");
                var result = await _userManager.ResetPasswordAsync(user, Input.Code, Input.Password);

                if (result.Succeeded)
                {
                    _logger.LogInformation("User reset password successfully.");
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
                TempData["Exception"] = @"Swal.fire({ icon: 'error', title: 'Error !', text: `" + errorStr.Replace("\\", "/") + @"`, showConfirmButton: true });";
                _logger.LogTrace("End reset password on post.");
                return Page();
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message.ToString());
                TempData["Exception"] = @"Swal.fire({ icon: 'error', title: 'Error !', text: `" + e.Message.Replace("\\", "/") + @"`, showConfirmButton: true });";
                _logger.LogTrace("End reset password on post.");
                return Page();
            } // End try catch
        } // End OnPostAsync
    } // End ResetPasswordModel
}
