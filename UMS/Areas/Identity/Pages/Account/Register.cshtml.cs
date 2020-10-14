using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using UMS.Areas.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authentication;
using System.ComponentModel.DataAnnotations;

/*
 * Name: RegisterModel.cs (Extend : PageModel)
 * Namespace: UMS.Areas.Identity.Pages.Account
 * Author: Idenity system
 */

namespace UMS.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        // Service
        private readonly ILogger<RegisterModel> _logger;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        // Attribute
        [BindProperty] 
        public InputModel Input { get; set; } // Model input 
        public string ReturnUrl { get; set; } // for url when redirect
        public IList<AuthenticationScheme> ExternalLogins { get; set; } // External info of login

        /*
         * Name: RegisterMode
         * Parametor: userManager(UserManager), signInManager(SignInManager), logger(ILogger)
         */
        public RegisterModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ILogger<RegisterModel> logger)
        {
            try
            {
                _userManager = userManager;
                _signInManager = signInManager;
                _logger = logger;
                _logger.LogDebug("Start Register model.");
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message.ToString());
                _logger.LogTrace("End Register model Constructor.");
            }// End try catch
        } // End Constructor

        /*
         * Name: InputModel
         * Description: The model for registration.
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
            [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[\:\;\[\]\\\|\/\.\,\'\""\()\{}\<>_#$!%@@^฿*?&\-\+\=])[A-Za-z0-9\:\;\[\]\\\|\/\.\,\'\""\()\{}\<>_#$!%@@^฿*?&\-\+\=]+$"
                , ErrorMessage = "The password must contain at least <br> 1 uppercase, 1 lowercase, 1 digit and 1 special character.")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirm password do not match.")]
            public string ConfirmPassword { get; set; }
        } // End Input Model

        /*
         * Name: OnGetAsync
         * Parameter: returnUrl(string)
         * Description: Setting a direction and getting information external login.
         */
        public async Task OnGetAsync(string returnUrl = null)
        {
            try
            {
                ReturnUrl = returnUrl;
                _logger.LogTrace("Getting external login.");
                ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
                _logger.LogTrace("Register on get.");
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message.ToString());
                TempData["Exception"] = @"Swal.fire({ icon: 'error', title: 'Error !', text: `" + e.Message.Replace("\\", "/") + @"`, showConfirmButton: true });";
                _logger.LogTrace("Register on get.");
            }
        } // End OnGetAsync

        /*
         * Name: OnPostAsync
         * Parameter: returnUrl(string)
         * Description: The registration of this system.
         */
        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            try
            {
                _logger.LogTrace("Start register on post.");
                returnUrl ??= Url.Content("~/");
                ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
                if (ModelState.IsValid)
                {
                    _logger.LogTrace("Creating application user.");
                    {
                        var user = new ApplicationUser
                        {
                            UserName = Input.Email,
                            Email = Input.Email,
                            acc_Firstname = Input.acc_Firstname,
                            acc_Lastname = Input.acc_Lastname,
                            acc_IsActive = 'Y',
                            ConcurrencyStamp = "dc294a4b-8d8a-49b0-8966-e23ff87778b0",
                            SecurityStamp = "OWWI2VTYV564YBQIUK3PI75QL7DA6NPJ"
                        }; // Create new user
                        _logger.LogTrace("Creating new user.");
                        var result = await _userManager.CreateAsync(user, Input.Password);
                        if (result.Succeeded) // Check if create success
                        {
                            _logger.LogInformation("User created a new account with password.");
                            _logger.LogDebug("Generating provider key.");
                            var userLogin = new ApplicationUser
                            {
                                UserName = Input.Email,
                                Email = Input.Email,
                                acc_Firstname = Input.acc_Firstname,
                                acc_Lastname = Input.acc_Lastname,
                                acc_IsActive = 'Y',
                                ConcurrencyStamp = "dc294a4b-8d8a-49b0-8966-e23ff87778b1",
                                SecurityStamp = "OWWI2VTYV564YBQIUK3PI75QL7DA6NPA"
                            }; // Create new user
                            UserLoginInfo info = new UserLoginInfo("Email", RandomString(50).ToString(), "Email");
                            result = await _userManager.AddLoginAsync(userLogin, info);
                            _logger.LogTrace("Add login.");
                            if (result.Succeeded)
                            {
                                ApplicationUser userId = await _userManager.FindByEmailAsync(Input.Email);
                                _logger.LogDebug("Add default role to user.");
                                await _userManager.AddToRoleAsync(userId, "User");
                                _logger.LogInformation("User created a new login.");
                                _logger.LogDebug("Signing in.");
                                await _signInManager.SignInAsync(user, false);
                                _logger.LogTrace("End register on post.");
                                return LocalRedirect(Url.Content("~/").ToString());
                            }
                            else
                            {
                                _logger.LogError(result.Errors.First().Description.ToString());
                                TempData["Exception"] =
                                    @"Swal.fire({ icon: 'error', title: 'Error !', text: `" + result.Errors.First().Description.ToString() + "`, showConfirmButton: true })";
                                _logger.LogTrace("End register on post.");
                                return Page();
                            } // End Check if add login success
                            //_logger.LogInformation("User created with a password.");
                            //_logger.LogDebug("Adding a default role for users.");
                            //var userCreated = await _userManager.FindByEmailAsync(Input.Email);
                            //result = await _userManager.AddToRoleAsync(userCreated, "User"); // Add role
                            //if (result.Succeeded)
                            //{
                            //    _logger.LogDebug("Creating a provider key.");
                            //    var info = new UserLoginInfo("Email", RandomString(50).ToString(), "Email");
                            //    result = await _userManager.AddLoginAsync(userCreated, info);
                            //    _logger.LogTrace("Add login.");
                            //    if (result.Succeeded)
                            //    {
                            //        _logger.LogInformation("User added successfully.");
                            //        _logger.LogDebug("Signing in.");
                            //        await _signInManager.SignInAsync(newuser, false);
                            //        _logger.LogTrace("End register on post.");
                            //        return LocalRedirect(Url.Content("~/").ToString());
                            //    } // Add login
                            //} // Add role

                            //_logger.LogError(result.Errors.First().Description.ToString());
                            //TempData["Exception"] =
                            //    @"Swal.fire({ icon: 'error', title: 'Error !', text: `" + result.Errors.First().Description.ToString().Replace("\\", "/") + "`, showConfirmButton: true });";
                            //_logger.LogTrace("End register on post.");
                            //return Page();
                        } // End if user create successful
                        string errorStr = "";
                        foreach (var error in result.Errors)
                        {
                            errorStr += error.Description + ((error.Code != "" && error.Code != null) ? " (" + error.Code + ")." : ".");
                            ModelState.AddModelError(string.Empty, error.Description);
                        } // End loop get error
                        _logger.LogError(errorStr.ToString());
                        TempData["Exception"] = @"Swal.fire({ icon: 'error', title: 'Error !', text: `" + errorStr.Replace("\\", "/") + @"`, showConfirmButton: true });";
                    } // End if user exist
                } // End if model is valid
                _logger.LogTrace("End register on post.");
                return Page();
            } 
            catch (Exception e)
            {
                _logger.LogError(e.Message.ToString());
                TempData["Exception"] = @"Swal.fire({ icon: 'error', title: 'Error !', text: `" + e.Message.Replace("\\", "/") + @"`, showConfirmButton: true });";
                _logger.LogTrace("End register on post.");
                return Page();
            } // End Try Catch
        } // End OnPostAsync

        private static Random random = new Random(); // for random provider key

        /*
         * Name: RandomString
         * Parameter: length(int)
         * Description: The randomization of a provider key
         */
        public static string RandomString(int length)
        {
            const string chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        } // End RandomString
    } // End Register
}
