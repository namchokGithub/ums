using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using UMS.Areas.Identity.Data;
using static UMS.Areas.Identity.Pages.Account.LoginModel;

/*
 * Name: AccountController.cs
 * Namespace: Controllers
 * Author: Wannapa Srijermtong
 */

namespace UMS.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        // Variable for manager
        private readonly ILogger<AccountController> _logger;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, ILogger<AccountController> logger)
        {
            try
            {
                _logger = logger;
                _logger.LogTrace("NLog injected into AccountController.");
                _signInManager = signInManager;
                _logger.LogTrace("Sign In Manager injected into AccountController.");
                _userManager = userManager;
                _logger.LogTrace("User Manager manager injected into AccountController.");
                _logger.LogTrace("Start AccountController Constructor.");
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message.ToString());
                _logger.LogTrace("End AccountController Constructor.");
            }// End try catch
        } // End consturcter

        /*
         * Name: Index
         * Parameter: none
         * Description: none
         */
        public IActionResult Index()
        {
            return View();
        } // End Index

        /*
         * Name: InputModel
         * Parameter: returnUrl(string)
         * Description: set return url
         */
        [AllowAnonymous]
        public IActionResult InputModel(string returnUrl)
        {
            InputModel login = new InputModel();
            login.ReturnUrl = returnUrl;
            return View(login);
        } // End InputModel

        /*
         * Name: InputModel (Overload)
         * Parameter: login(InputModel)
         * Description: for log in
         */
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> InputModel(InputModel login)
        {
            try
            {
                _logger.LogTrace("Start Input Model.");
                if (ModelState.IsValid)
                {
                    _logger.LogDebug("Finding Email.");
                    ApplicationUser appUser = await _userManager.FindByEmailAsync(login.Email);
                    if (appUser != null)
                    {
                        _logger.LogDebug("Signing out.");
                        await _signInManager.SignOutAsync();
                        _logger.LogDebug("Signing in with password.");
                        Microsoft.AspNetCore.Identity.SignInResult result = await _signInManager.PasswordSignInAsync(appUser, login.Password, false, false);
                        if (result.Succeeded)
                        {
                            _logger.LogInformation("Logged In.");
                            _logger.LogTrace("End Input Model.");
                            return Redirect(login.ReturnUrl ?? "/");
                        } // If logged in
                    }
                    Console.WriteLine("Login Failed: Invalid Email or password.");
                    ModelState.AddModelError(nameof(login.Email), "Login Failed: Invalid Email or password.");
                }
                _logger.LogTrace("End Input Model.");
                return View(login);
            } catch (Exception e)
            {
                _logger.LogError(e.Message.ToString());
                string message = @"Swal.fire({ icon: 'error', title: 'Error !', text: `" + e.Message + @"`, showConfirmButton: true })";
                TempData["Exception"] = message;
                _logger.LogTrace("End Input Model.");
                return RedirectToPage("./Login");
            } // End try catch
        } // End InputModel

        /*
         * Name: Logout
         * Parameter: none
         * Description: log out
         */
        public async Task<IActionResult> Logout()
        {
            try
            {
                await _signInManager.SignOutAsync();
                _logger.LogInformation("Logged out.");
                return RedirectToPage("./Login");
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message.ToString());
                string message = @"Swal.fire({ icon: 'error', title: 'Error !', text: `" + e.Message + @"`, showConfirmButton: true })";
                TempData["Exception"] = message;
                return RedirectToPage("./Login");
            } // End try catch
        } // End Logout

        /*
         * Name: AccessDenied
         * Parameter: none
         * Description: for authen page
         */
        public IActionResult AccessDenied()
        {
            _logger.LogWarning("Access Denied.");
            return View();
        } // End AccessDenied

        /*
         * Name: GoogleLogin
         * Parameter: none
         * Description: get properties
         */
        [AllowAnonymous]
        public IActionResult GoogleLogin()
        {
            try
            {
                _logger.LogTrace("Start Google Login.");
                string redirectUrl = Url.Action("GoogleResponse", "Account");
                var properties = _signInManager.ConfigureExternalAuthenticationProperties("Google", redirectUrl);
                _logger.LogTrace("Get properties from google.");
                _logger.LogTrace("End Google Login.");
                return new ChallengeResult("Google", properties);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message.ToString());
                string message = @"Swal.fire({ icon: 'error', title: 'Error !', text: `" + e.Message + @"`, showConfirmButton: true })";
                TempData["Exception"] = message;
                _logger.LogTrace("End Google Login.");
                return RedirectToPage("./Login");
            } // End try catch
            
        } // End GoogleLogin

        /*
         * Name: GoogleResponse
         * Parameter: none
         * Description: login or create new user
         */
        [AllowAnonymous]
        public async Task<IActionResult> GoogleResponse()
        {
            try
            {
                _logger.LogTrace("Start Google Response.");
                ExternalLoginInfo info = await _signInManager.GetExternalLoginInfoAsync();
                _logger.LogDebug("Get external log in info.");
                if (info == null)
                {
                    _logger.LogWarning("User info is null.");
                    _logger.LogTrace("End Google Response.");
                    return RedirectToAction(nameof(InputModel));
                }
                _logger.LogDebug("Getting external log in and sign in.");
                var result = await _signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, false);
                _logger.LogTrace("Creating user info.");
                string[] userInfo = {
                    info.Principal.FindFirst(ClaimTypes.Name).Value,
                    info.Principal.FindFirst(ClaimTypes.Email).Value,
                    info.Principal.FindFirst(ClaimTypes.GivenName).Value,
                    info.Principal.FindFirst(ClaimTypes.Surname).Value
                }; // Craete user info

                if (result.Succeeded)
                {
                    _logger.LogInformation("Log In Successfully.");
                    _logger.LogTrace("End Google Response.");
                    return RedirectToAction("Index", "Home");
                }// If had account
                else
                {
                    _logger.LogTrace("Creating appliction user.");
                    ApplicationUser user = new ApplicationUser
                    {
                        Email = info.Principal.FindFirst(ClaimTypes.Email).Value,
                        UserName = info.Principal.FindFirst(ClaimTypes.Email).Value,
                        acc_Firstname = info.Principal.FindFirst(ClaimTypes.GivenName).Value,
                        acc_Lastname = info.Principal.FindFirst(ClaimTypes.Surname).Value,
                        acc_IsActive = 'Y'
                    }; // create new user
                    _logger.LogTrace("Creating user info.");
                    IdentityResult identResult = await _userManager.CreateAsync(user);
                    if (identResult.Succeeded)
                    {
                        ApplicationUser userId = await _userManager.FindByEmailAsync(user.Email);
                        await _userManager.AddToRoleAsync(userId, "User");
                        _logger.LogInformation("Create user succeeded.");
                        _logger.LogDebug("Adding login.");
                        identResult = await _userManager.AddLoginAsync(user, info);
                        if (identResult.Succeeded)
                        {
                            _logger.LogDebug("Signing in.");
                            await _signInManager.SignInAsync(user, false);
                            _logger.LogInformation("Add login succeeded.");
                            _logger.LogTrace("End Google Response.");
                            return RedirectToAction("Index", "Home");
                        }
                    } // If success add login info
                    _logger.LogTrace("End Google Response.");
                    return AccessDenied();
                } // End if signed in success
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message.ToString());
                string message = @"Swal.fire({ icon: 'error', title: 'Error !', text: `" + e.Message + @"`, showConfirmButton: true })";
                TempData["Exception"] = message;
                _logger.LogTrace("End Google Response.");
                return RedirectToPage("./Login");
            } // End try catch
        } // End GoogleResponse

        /*
         * Name: FacebookLogin
         * Parameter: none
         * Description: get properties
         */
        [AllowAnonymous]
        public IActionResult FacebookLogin()
        {
            try
            {
                _logger.LogTrace("Start Facebook Login");
                string redirectUrl = Url.Action("FacebookResponse", "Account");
                var properties = _signInManager.ConfigureExternalAuthenticationProperties("Facebook", redirectUrl);
                _logger.LogTrace("Get properties from properties.");
                _logger.LogTrace("End Facebook Login");
                return new ChallengeResult("Facebook", properties);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message.ToString());
                string message = @"Swal.fire({ icon: 'error', title: 'Error !', text: `" + e.Message + @"`, showConfirmButton: true })";
                TempData["Exception"] = message;
                _logger.LogTrace("End Facebook Login");
                return RedirectToPage("./Login");
            } // End try catch
        } // End FacebookLogin

        /*
         * Name: FacebookResponse
         * Parameter: none
         * Description: login or new user
         */
        [AllowAnonymous]
        public async Task<IActionResult> FacebookResponse()
        {
            try
            {
                _logger.LogTrace("Start Facebook Response.");
                _logger.LogDebug("Getting user info.");
                ExternalLoginInfo info = await _signInManager.GetExternalLoginInfoAsync();
                if (info == null)
                {
                    _logger.LogWarning("User info is null.");
                    _logger.LogTrace("End Facebook Response.");
                    return RedirectToAction(nameof(InputModel));
                }
                _logger.LogDebug("Getting result external log in and sign in.");
                var result = await _signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, false);
                _logger.LogTrace("Craete user info.");
                string[] userInfo = {
                    info.Principal.FindFirst(ClaimTypes.Name).Value,
                    info.Principal.FindFirst(ClaimTypes.Email).Value,
                    info.Principal.FindFirst(ClaimTypes.GivenName).Value,
                    info.Principal.FindFirst(ClaimTypes.Surname).Value
                }; // Crate new user info
                if (result.Succeeded)
                {
                    _logger.LogInformation("Log in succeeded.");
                    _logger.LogTrace("End Facebook Response.");
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    _logger.LogTrace("Creating application user.");
                    ApplicationUser user = new ApplicationUser
                    {
                        Email = info.Principal.FindFirst(ClaimTypes.Email).Value,
                        UserName = info.Principal.FindFirst(ClaimTypes.Email).Value,
                        acc_Firstname = info.Principal.FindFirst(ClaimTypes.GivenName).Value,
                        acc_Lastname = info.Principal.FindFirst(ClaimTypes.Surname).Value,
                        acc_IsActive = 'Y'
                    }; // Create new user
                    _logger.LogDebug("Creating user.");
                    IdentityResult identResult = await _userManager.CreateAsync(user);
                    if (identResult.Succeeded)
                    {
                        ApplicationUser userId = await _userManager.FindByEmailAsync(user.Email);
                        await _userManager.AddToRoleAsync(userId, "User");
                        _logger.LogTrace("Create user succeeded.");
                        _logger.LogDebug("Craeting user.");
                        identResult = await _userManager.AddLoginAsync(user, info);
                        if (identResult.Succeeded)
                        {
                            _logger.LogDebug("Signing in.");
                            await _signInManager.SignInAsync(user, false);
                            _logger.LogInformation("Add log in succeeded.");
                            _logger.LogTrace("End Facebook Response.");
                            return RedirectToAction("Index", "Home");
                        }
                    } // create and add login info
                    _logger.LogTrace("End Facebook Response.");
                    return AccessDenied();
                } // End if signed in success
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message.ToString());
                string message = @"Swal.fire({ icon: 'error', title: 'Error !', text: `" + e.Message + @"`, showConfirmButton: true })";
                TempData["Exception"] = message;
                _logger.LogTrace("End Facebook Response.");
                return RedirectToPage("./Login");
            } // End try catch
        } // End FacebookResponse

        /*
         * Name: MicrosoftLogin
         * Parameter: none
         * Description: get properties
         */
        [AllowAnonymous]
        public IActionResult MicrosoftLogin()
        {
            try
            {
                _logger.LogTrace("Start Microsoft Login.");
                string redirectUrl = Url.Action("MicrosoftResponse", "Account");
                _logger.LogDebug("Getting properties from Microsoft.");
                var properties = _signInManager.ConfigureExternalAuthenticationProperties("Microsoft", redirectUrl);
                _logger.LogTrace("End Microsoft Login.");
                return new ChallengeResult("Microsoft", properties);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message.ToString());
                string message = @"Swal.fire({ icon: 'error', title: 'Error !', text: `" + e.Message + @"`, showConfirmButton: true })";
                TempData["Exception"] = message;
                _logger.LogTrace("End Microsoft Login.");
                return RedirectToPage("./Login");
            } // End try catch
        } // End MicrosoftLogin

        /*
         * Name: MicrosoftResponse
         * Parameter: none
         * Description: login or create new user
         */
        [AllowAnonymous]
        public async Task<IActionResult> MicrosoftResponse()
        {
            try
            {
                _logger.LogTrace("Start Microsoft Response.");
                ExternalLoginInfo info = await _signInManager.GetExternalLoginInfoAsync();
                _logger.LogDebug("Get external log in.");
                if (info == null)
                {
                    _logger.LogWarning("User info is null.");
                    _logger.LogTrace("End Microsoft Response.");
                    return RedirectToAction(nameof(InputModel));
                }
                _logger.LogDebug("Getting External log in and sign in.");
                var result = await _signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, false);
                _logger.LogTrace("Creating user info object.");
                string[] userInfo = {
                    info.Principal.FindFirst(ClaimTypes.Name).Value,
                    info.Principal.FindFirst(ClaimTypes.Email).Value,
                    info.Principal.FindFirst(ClaimTypes.GivenName).Value,
                    info.Principal.FindFirst(ClaimTypes.Surname).Value
                }; // Create new user info
                if (result.Succeeded)
                {
                    _logger.LogInformation("Log in succeeded.");
                    _logger.LogTrace("End Microsoft Response.");
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    _logger.LogTrace("Creating Applicaiton user.");
                    ApplicationUser user = new ApplicationUser
                    {
                        Email = info.Principal.FindFirst(ClaimTypes.Email).Value,
                        UserName = info.Principal.FindFirst(ClaimTypes.Email).Value,
                        acc_Firstname = info.Principal.FindFirst(ClaimTypes.GivenName).Value,
                        acc_Lastname = info.Principal.FindFirst(ClaimTypes.Surname).Value,
                        acc_IsActive = 'Y'
                    }; // Create new user

                    _logger.LogDebug("Creating user.");
                    IdentityResult identResult = await _userManager.CreateAsync(user);
                    if (identResult.Succeeded)
                    {
                        _logger.LogInformation("Create user succeeded.");
                        identResult = await _userManager.AddLoginAsync(user, info);
                        _logger.LogDebug("Adding log in.");
                        if (identResult.Succeeded)
                        {
                            ApplicationUser userId = await _userManager.FindByEmailAsync(user.Email);
                            await _userManager.AddToRoleAsync(userId, "User");
                            _logger.LogDebug("Singing in.");
                            await _signInManager.SignInAsync(user, false);
                            _logger.LogInformation("Add log in succeeded.");
                            _logger.LogTrace("End Microsoft Response.");
                            return RedirectToAction("Index", "Home");
                        }
                    } // Crate user and add login info
                    _logger.LogTrace("End Microsoft Response.");
                    return AccessDenied();
                } // End if signed in success
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message.ToString());
                string message = @"Swal.fire({ icon: 'error', title: 'Error !', text: `" + e.Message + @"`, showConfirmButton: true })";
                TempData["Exception"] = message;
                _logger.LogTrace("End Microsoft Response.");
                return RedirectToPage("./Login");
            } // End try catch
        } // End MicrosoftResponse
    } // End AccountController
}
