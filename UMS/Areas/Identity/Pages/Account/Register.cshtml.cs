using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using UMS.Areas.Identity.Data;
using UMS.Models;

/*
 * Name: RegisterModel.cs
 * Namespace: UMS.Areas.Identity.Pages.Account
 * Author: Idenity system
 */

namespace UMS.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        // Service
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;
        private readonly AccountContext _accountContext;
        // Attribute
        [BindProperty]
        public InputModel Input { get; set; } // Model input 
        public string ReturnUrl { get; set; } // for url when redirect
        public IList<AuthenticationScheme> ExternalLogins { get; set; } // External info of login

        /*
         * Name: RegisterMode
         * Parametor: userManager(UserManager), signInManager(SignInManager), logger(ILogger), emailSender(IEmailSender), accountContext(AccountContext)
         * Description: constructor
         */
        public RegisterModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ILogger<RegisterModel> logger,
            AccountContext accountContext,
            IEmailSender emailSender)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
            _accountContext = accountContext;
            _logger.LogDebug("Register model.");
        } // End Constructor

        /*
         * Name: InputModel
         * Description: Model for register
         */
        public class InputModel
        {
            [Required]
            [DataType(DataType.Text)]
            [Display(Name = "First Name")]
            [RegularExpression(@"^[a-zA-Z]+(([a-zA-Z])?[a-zA-Z]*)*$"
                , ErrorMessage = "The First name must only character.")]
            public string acc_Firstname { get; set; }

            [Required]
            [DataType(DataType.Text)]
            [Display(Name = "Last Name")]
            [RegularExpression(@"^[a-zA-Z]+(([a-zA-Z])?[a-zA-Z]*)*$"
                , ErrorMessage = "The Last name must only character.")]
            public string acc_Lastname { get; set; }

            [Required]
            [RegularExpression(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$"
                , ErrorMessage = "The Email is not valid.")]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            [StringLength(100
                , ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[$@$!%*#?&])[A-Za-z0-9$@$!%*#?&]+$"
                ,ErrorMessage = "The password must contain at least <br> 1 uppercase, 1 lowercase, 1 digit and 1 special character.")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirm password do not match.")]
            public string ConfirmPassword { get; set; }
        } // End Input Model

        /*
         * Name: OnGetAsync
         * Parameter: returnUrl(string)
         * Description: Set return url and get external login
         */
        public async Task OnGetAsync(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            _logger.LogTrace("Register on get.");
        } // End OnGetAsync

        /*
         * Name: OnPostAsync
         * Parameter: returnUrl(string)
         * Description: Set return url and get external login
         */
        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            try
            {
                _logger.LogTrace("Start register on post.");
                returnUrl = returnUrl ?? Url.Content("~/");
                ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
                if (ModelState.IsValid)
                {
                    _logger.LogTrace("Creating application user.");
                    var user = new ApplicationUser
                    {
                        UserName = Input.Email,
                        Email = Input.Email,
                        acc_Firstname = Input.acc_Firstname,
                        acc_Lastname = Input.acc_Lastname,
                        acc_IsActive = 'Y'
                    }; // New user
                    _logger.LogTrace("Creating user.");
                    var result = await _userManager.CreateAsync(user, Input.Password);
                    // Check if create success
                    if (result.Succeeded)
                    {
                        ApplicationUser userId = await _userManager.FindByEmailAsync(Input.Email);
                        await _userManager.AddToRoleAsync(userId, "User");
                        _logger.LogInformation("User created a new account with password.");
                        _logger.LogDebug("Generating provider key.");
                        var info = new UserLoginInfo("Email", RandomString(50).ToString(), "Email");
                        result = await _userManager.AddLoginAsync(user, info);
                        _logger.LogTrace("Add login.");
                        if (result.Succeeded)
                        {
                            _logger.LogInformation("User created a new log in.");
                            await _signInManager.SignInAsync(user, false);
                            _logger.LogTrace("End register on post.");
                            return LocalRedirect(returnUrl);
                        }
                        else
                        {
                            _logger.LogWarning("Log in Already Associated. A user with this login already exists.");
                            TempData["Exception"] =
                                @"Swal.fire({ icon: 'warning', title: 'Error !', text: 'Log in Already Associated. A user with this login already exists.', showConfirmButton: true })";
                        } // End Check if add login success
                        _logger.LogTrace("Signing in.");
                        await _signInManager.SignInAsync(user, isPersistent: false);
                        _logger.LogTrace("End register on post.");
                        return LocalRedirect(returnUrl);
                    } // End if create success
                    string errorStr = "";
                    foreach (var error in result.Errors)
                    {
                        errorStr += error.Description + " (" + error.Code + "). ";
                        ModelState.AddModelError(string.Empty, error.Description);
                    } // End loop get error
                    _logger.LogError(errorStr.ToString());
                    TempData["Exception"] = @"Swal.fire({ icon: 'error', title: 'Error !', text: `" + errorStr + @"`, showConfirmButton: true })";
                } // End if model is valid
                _logger.LogTrace("End register on post.");
                return Page();
            } 
            catch (Exception e)
            {
                _logger.LogError(e.Message.ToString());
                TempData["Exception"] = @"Swal.fire({ icon: 'error', title: 'Error !', text: `" + e.Message + @"`, showConfirmButton: true })";
                _logger.LogTrace("End register on post.");
                return Page();
            } // End Try Catch
        } // End OnPostAsync

        private static Random random = new Random(); // for random provider key

        /*
         * Name: RandomString
         * Parameter: length(int)
         * Description: For random provider key
         */
        public static string RandomString(int length)
        {
            const string chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        } // End RandomString
    } // End Register
}
