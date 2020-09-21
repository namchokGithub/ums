using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using UMS.Areas.Identity.Data;
using System;
using UMS.Controllers;
using UMS.Models;

/*
 * Name: LoginModel.cs
 * Namespace: UMS.Areas.Identity.Pages.Account
 * Author: Idenity system
 */

namespace UMS.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class LoginModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger<LoginModel> _logger;
        private readonly ManageUserController _manageUser;

        public LoginModel(
            SignInManager<ApplicationUser> signInManager,
            ILogger<LoginModel> logger,
            ILogger<ManageUserController> loggerManageUser,
            EditAccountContext editaccountContext,
            AccountContext accountContext,
            UserManager<ApplicationUser> userManager)
        {
            try
            {
                _userManager = userManager;
                _signInManager = signInManager;
                _logger = logger;
                _manageUser = new ManageUserController(accountContext, editaccountContext, loggerManageUser, signInManager);
                _logger.LogDebug("Starting Login model.");
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message.ToString());
                _logger.LogTrace("End Login model Constructor.");
            }// End try catch
        } // End constructor

        [BindProperty]
        public InputModel Input { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public string ReturnUrl { get; set; }

        [TempData]
        public string ErrorMessage { get; set; }

        /*
         * Name: InputModel
         * Description: for input
         */
        public class InputModel
        {
            [Required]
            [RegularExpression(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$"
                , ErrorMessage = "The Email is not valid.")]
            public string Email { get; set; }

            [Required]
            //[DataType(DataType.Password)]
            [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[\[\]\\\|\/\.\,\'\""\()\{}\<>_#$!%@@^฿*?&\-\+\=])[A-Za-z0-9\[\]\\\|\/\.\,\'\""\()\{}\<>_#$!%@@^฿*?&\-\+\=]+$"
                , ErrorMessage = "The password must contain at least <br> 1 uppercase, 1 lowercase, 1 digit and 1 special character.")]
            public string Password { get; set; }

            [Display(Name = "Remember me?")]
            public bool RememberMe { get; set; }

            public string ReturnUrl { get; set; }
        } // End InputModel

        /*
         * Name: OnGetAsync
         * Parameter: returnUrl(String)
         * Description: Check if logged in.
         */
        public async Task OnGetAsync(string returnUrl = null)
        {
            try
            {
                _logger.LogTrace("Start login on get.");
                if (User.Identity.IsAuthenticated)
                {
                    _logger.LogInformation("User is authenticated.");
                    Response.Redirect("/");
                } // Check if logged in
                if (!string.IsNullOrEmpty(ErrorMessage))
                {
                    _logger.LogError(ErrorMessage.ToString());
                    ModelState.AddModelError(string.Empty, ErrorMessage);
                } // check if has error message
                returnUrl = returnUrl ?? Url.Content("~/");  // Clear the existing external cookie to ensure a clean login process
                _logger.LogTrace("Signing out.");
                await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);
                _logger.LogDebug("Getting external authentication scheme.");
                ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
                ReturnUrl = returnUrl;
                _logger.LogTrace("End login on get.");
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message.ToString());
                string message = @"Swal.fire({ icon: 'error', title: 'Error !', text: `" + e.Message + @"`, showConfirmButton: true })";
                TempData["Exception"] = message;
                _logger.LogTrace("End login on get.");
            } // End try catch
        } // End OnGetAsync

        /*
         * Name: OnPostAsync
         * Parameter: returnUrl(String)
         * Description: for login to system
         */
        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            try
            {
                _logger.LogTrace("Start login on post.");
                returnUrl = returnUrl ?? Url.Content("~/");
                if (ModelState.IsValid)
                {
                    _logger.LogTrace("Signing in with password.");
                    var result = await _signInManager.PasswordSignInAsync(Input.Email, Input.Password, Input.RememberMe, lockoutOnFailure: false);
                    if (result.Succeeded)
                    {
                        _logger.LogInformation("User logged in.");
                        ApplicationUser user = await _userManager.FindByEmailAsync(Input.Email.ToString());
                        if (user.acc_IsActive == 'N')
                        {
                            _manageUser.deleteUser(user.Id);
                            _logger.LogInformation("Change status Inactive to active user.");
                        }// End check status
                        _logger.LogTrace("End login on post.");
                        return LocalRedirect(returnUrl);
                    } // If login success
                    else if (result.IsLockedOut)
                    {
                        _logger.LogWarning("User account locked out.");
                        _logger.LogTrace("End login on post.");
                        return RedirectToPage("/Lockout");
                    }
                    else
                    {
                        _logger.LogError("Your email or password is not valid.");
                        ModelState.AddModelError(string.Empty, "Your email or password is not valid.");
                        // Send alert to home pages
                        TempData["ExceptionInValid"] = "InValid";
                        // TempData["Exception"] = @"toastr.error('Your email or password is not valid.')";
                        _logger.LogTrace("End login on post.");
                        return Page();
                    } // If Loged out
                } // End if check modelState
                _logger.LogTrace("End login on post.");
                return Page();
            } catch (Exception e)
            {
                _logger.LogError(e.Message.ToString());
                string message = @"Swal.fire({ icon: 'error', title: 'Error !', text: `" + e.Message + @"`, showConfirmButton: true })";
                TempData["Exception"] = message;
                _logger.LogTrace("End login on post.");
                return Page();
            } // End try catch
        } // End OnPostAsync
    } // End login
}
