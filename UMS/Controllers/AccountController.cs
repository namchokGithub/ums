using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using UMS.Areas.Identity.Data;
using UMS.Models;
using static UMS.Areas.Identity.Pages.Account.LoginModel;

/*
 * Name: AccountController.cs
 * Author: Wannapa Srijermtong
 * Description: The controller manages an account login.
 */

namespace UMS.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly ILogger<AccountController> _logger;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ManageUserController _manageUserController;
        /*
         * Name: AccountController
         * Parameter: userManager, accountContext, editaccountContext, loggerManager, signInManager, logger
         * Description: Controller manages an account login.
         */
        public AccountController(UserManager<ApplicationUser> userManager,
            AccountContext accountContext,
            EditAccountContext editaccountContext,
            ILogger<ManageUserController> loggerManager,
            SignInManager<ApplicationUser> signInManager,
            ILogger<AccountController> logger)
        {
            try
            {
                _logger = logger;
                _signInManager = signInManager;
                _userManager = userManager;
                _manageUserController = new ManageUserController(accountContext, editaccountContext, loggerManager, _signInManager);
                _logger.LogTrace("Start account controller.");
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message.ToString());
                _logger.LogTrace("End account controller.");
            }// End try catch
        } // End consturcter

        /*
         * Name: Index
         * Author: Wannapa Srijermtong
         */
        public IActionResult Index() { return View(); } // End Index

        /*
         * Name: InputModel
         * Parameter: returnUrl(string)
         * Author: Wannapa Srijermtong
         * Description: Setting path url.
         */
        [AllowAnonymous]
        public IActionResult InputModel(string returnUrl)
        {
            InputModel login = new InputModel { ReturnUrl = returnUrl };
            return View(login);
        } // End InputModel

        /*
         * Name: InputModel (Overload)
         * Parameter: login(InputModel)
         * Author: Wannapa Srijermtong
         * Description: Function for login.
         */
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> InputModel(InputModel login)
        {
            try
            {
                _logger.LogTrace("Start input model.");
                if (ModelState.IsValid)
                {
                    _logger.LogDebug("Finding an email.");
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
                            _logger.LogTrace("End input model.");
                            return Redirect(login.ReturnUrl ?? $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}/");
                        } // If logged in
                    }
                    ModelState.AddModelError(nameof(login.Email), "Login Failed: The input invalid an email or password.");
                }
                _logger.LogTrace("End input model.");
                return View(login);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message.ToString());
                TempData["Exception"] = @"Swal.fire({ icon: 'error', title: 'Error !', text: `" + e.Message.Replace("\\", "/") + @"`, showConfirmButton: true });";
                _logger.LogTrace("End input model.");
                return RedirectToPage("/Login");
            } // End try catch
        } // End InputModel

        /*
         * Name: Logout
         * Author: Wannapa Srijermtong
         * Description: Function for logout of the system and Redirect to login page.
         */
        public async Task<IActionResult> Logout()
        {
            try
            {
                await _signInManager.SignOutAsync();
                _logger.LogInformation("Logged out.");
                return Redirect($"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}/Identity/Account/Logout");
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message.ToString());
                TempData["Exception"] = @"Swal.fire({ icon: 'error', title: 'Error !', text: `" + e.Message.Replace("\\", "/") + @"`, showConfirmButton: true });";
                return Redirect($"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}/Identity/Account/Logout");
            } // End try catch
        } // End Logout

        /*
         * Name: AccessDenied
         * Author: Wannapa Srijermtong
         * Description: Redirect to access denied page.
         */
        public IActionResult AccessDenied()
        {
            ViewData["URL"] = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";
            ViewData["URLHOME"] = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}/Home";
            _logger.LogWarning("Access Denied.");
            return View();
        } // End AccessDenied

        /*
         * Name: GoogleLogin
         * Author: Wannapa Srijermtong
         * Description: Get properties of authentication.
         */
        [AllowAnonymous]
        public IActionResult GoogleLogin()
        {
            try
            {
                _logger.LogTrace("Start google login.");
                string redirectUrl = Url.Action("GoogleResponse", "Account");
                var properties = _signInManager.ConfigureExternalAuthenticationProperties("Google", redirectUrl);
                _logger.LogTrace("Getting properties from google.");
                _logger.LogTrace("End google login.");
                return new ChallengeResult("Google", properties);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message.ToString());
                TempData["Exception"] = @"Swal.fire({ icon: 'error', title: 'Error !', text: `" + e.Message.Replace("\\", "/") + @"`, showConfirmButton: true});";
                _logger.LogTrace("End google login.");
                return Redirect($"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}/Identity/Account/Logout");
            } // End try catch
        } // End GoogleLogin

        /*
         * Name: GoogleResponse
         * Author: Wannapa Srijermtong
         * Description: Creating an account or login to UMS.
         */
        [AllowAnonymous]
        public async Task<IActionResult> GoogleResponse()
        {
            try
            {
                _logger.LogTrace("Start google response.");
                ViewData["URL"] = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";
                ViewData["URLHOME"] = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}/Home";

                _logger.LogDebug("Getting external login info.");
                ExternalLoginInfo info = await _signInManager.GetExternalLoginInfoAsync();
                if (info == null) throw new Exception("User information not found.");

                _logger.LogDebug("Getting result external login and sign in.");
                var result = await _signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, true);
                _logger.LogTrace("Creating user information.");
                string[] userInfo = {
                    info.Principal.FindFirst(ClaimTypes.Name).Value,
                    info.Principal.FindFirst(ClaimTypes.Email).Value,
                    info.Principal.FindFirst(ClaimTypes.GivenName).Value,
                    info.Principal.FindFirst(ClaimTypes.Surname).Value
                }; // Craete user info

                if (result.Succeeded)
                {
                    var IsActive = _manageUserController.GetStatusUser(info.Principal.FindFirst(ClaimTypes.Email).Value);
                    if (IsActive.Value.ToString() == "ACTIVE")
                    {
                        _logger.LogInformation("Login successfully.");
                        _logger.LogTrace("End google response.");
                        return RedirectToAction("Index", "Home");
                    }
                    else if (IsActive.Value.ToString() == "INACTIVE")
                    {
                        _logger.LogInformation("This user is not active.");
                        _logger.LogDebug("Getting user by email.");
                        ApplicationUser user = await _userManager.FindByEmailAsync(info.Principal.FindFirst(ClaimTypes.Email).Value);
                        if (user == null) throw new Exception("Calling a method on a null object reference.");
                        _manageUserController.deleteUser(user.Id); // Toggle user status
                        _logger.LogInformation("Change status inactive to active user.");
                        _logger.LogTrace("End google response.");
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        throw new Exception("The user not found.");
                    } // End check user is inactive
                } // If you had an account
                else
                {
                    _logger.LogTrace("Creating an application user.");
                    ApplicationUser user = new ApplicationUser
                    {
                        Email = info.Principal.FindFirst(ClaimTypes.Email).Value,
                        UserName = info.Principal.FindFirst(ClaimTypes.Email).Value,
                        acc_Firstname = info.Principal.FindFirst(ClaimTypes.GivenName).Value,
                        acc_Lastname = info.Principal.FindFirst(ClaimTypes.Surname).Value,
                        acc_IsActive = 'Y'
                    }; // create new user

                    _logger.LogDebug("Creating user information.");
                    IdentityResult identResult = await _userManager.CreateAsync(user);
                    if (identResult.Succeeded)
                    {
                        _logger.LogDebug("Finding user by email.");
                        ApplicationUser userId = await _userManager.FindByEmailAsync(user.Email);
                        await _userManager.AddToRoleAsync(userId, "User");
                        _logger.LogInformation("User created successfully.");
                        _logger.LogDebug("Creating an user information.");
                        _logger.LogDebug("Adding login information.");
                        identResult = await _userManager.AddLoginAsync(user, info);
                        if (identResult.Succeeded)
                        {
                            _logger.LogDebug("Signing in.");
                            await _signInManager.SignInAsync(user, false);
                            _logger.LogInformation("Login successfully added.");
                            _logger.LogTrace("End google response.");
                            return RedirectToAction("Index", "Home");
                        } // End logged in successfully
                    } // End created successfully
                    _logger.LogError(identResult.Errors.First().Description.ToString());
                    TempData["Exception"] = @"Swal.fire({
                                                title: 'The " + user.Email + @" is already registered.',
                                                text: 'Would you like to try login instead?',
                                                icon: 'warning',
                                                showCancelButton: false,
                                                confirmButtonColor: '#3085d6',
                                                confirmButtonText: 'Login'
                                            }).then((res) => {
                                                console.log('Confirmed');
                                                window.location.href = '" + $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}/Identity/Account/Login"
                                                + "'});";
                    _logger.LogTrace("End google response.");
                    return View();
                } // End signed in successfully
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message.ToString());
                TempData["Exception"] = @"Swal.fire({ icon: 'error', title: 'Error !', text: `" + e.Message.Replace("\\", "/") + @"`, showConfirmButton: true });";
                _logger.LogTrace("End google response.");
                return Redirect($"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}/Identity/Account/Logout");
            } // End try catch
        } // End GoogleResponse

        /*
         * Name: FacebookLogin
         * Author: Wannapa Srijermtong
         * Description: Getting properties of authentication.
         */
        [AllowAnonymous]
        public IActionResult FacebookLogin()
        {
            try
            {
                _logger.LogTrace("Start facebook login");
                string redirectUrl = Url.Action("FacebookResponse", "Account");
                var properties = _signInManager.ConfigureExternalAuthenticationProperties("Facebook", redirectUrl);
                _logger.LogTrace("Getting properties from facebook.");
                _logger.LogTrace("End facebook login");
                return new ChallengeResult("Facebook", properties);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message.ToString());
                TempData["Exception"] = @"Swal.fire({ icon: 'error', title: 'Error !', text: `" + e.Message.Replace("\\", "/") + @"`, showConfirmButton: true });";
                _logger.LogTrace("End facebook login");
                return Redirect($"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}/Identity/Account/Logout");
            } // End try catch
        } // End FacebookLogin

        /*
         * Name: FacebookResponse
         * Author: Wannapa Srijermtong
         * Description: Creating an account or login to UMS.
         */
        [AllowAnonymous]
        public async Task<IActionResult> FacebookResponse()
        {
            try
            {
                _logger.LogTrace("Start facebook response.");
                ViewData["URL"] = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";
                ViewData["URLHOME"] = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}/Home";

                _logger.LogDebug("Getting user information.");
                ExternalLoginInfo info = await _signInManager.GetExternalLoginInfoAsync();
                if (info == null) throw new Exception("User information not found.");

                _logger.LogDebug("Getting external login and sign in.");
                var result = await _signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, true);
                _logger.LogTrace("Creating user information.");
                string[] userInfo = {
                    info.Principal.FindFirst(ClaimTypes.Name).Value,
                    info.Principal.FindFirst(ClaimTypes.Email).Value,
                    info.Principal.FindFirst(ClaimTypes.GivenName).Value,
                    info.Principal.FindFirst(ClaimTypes.Surname).Value
                }; // Crate new user info

                if (result.Succeeded)
                {
                    var IsActive = _manageUserController.GetStatusUser(info.Principal.FindFirst(ClaimTypes.Email).Value);
                    if (IsActive.Value.ToString() == "ACTIVE")
                    {
                        _logger.LogInformation("Login successfully.");
                        _logger.LogTrace("End facebook response.");
                        return RedirectToAction("Index", "Home");
                    }
                    else if (IsActive.Value.ToString() == "INACTIVE")
                    {
                        _logger.LogInformation("This user is not active.");
                        _logger.LogDebug("Getting user by email.");
                        ApplicationUser user = await _userManager.FindByEmailAsync(info.Principal.FindFirst(ClaimTypes.Email).Value);
                        if (user == null) throw new Exception("Calling a method on a null object reference.");
                        _manageUserController.deleteUser(user.Id); // Toggle user status
                        _logger.LogInformation("Change status inactive to active user.");
                        _logger.LogTrace("End facebook response.");
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        throw new Exception("The user not found.");
                    } // End check user is inactive
                } // If you had an account
                else
                {
                    _logger.LogTrace("Creating an application user.");
                    ApplicationUser user = new ApplicationUser
                    {
                        Email = info.Principal.FindFirst(ClaimTypes.Email).Value,
                        UserName = info.Principal.FindFirst(ClaimTypes.Email).Value,
                        acc_Firstname = info.Principal.FindFirst(ClaimTypes.GivenName).Value,
                        acc_Lastname = info.Principal.FindFirst(ClaimTypes.Surname).Value,
                        acc_IsActive = 'Y'
                    }; // Create new user

                    _logger.LogDebug("Creating user information.");
                    IdentityResult identResult = await _userManager.CreateAsync(user);
                    if (identResult.Succeeded)
                    {
                        _logger.LogDebug("Finding user by email.");
                        ApplicationUser userId = await _userManager.FindByEmailAsync(user.Email);
                        await _userManager.AddToRoleAsync(userId, "User");
                        _logger.LogInformation("User created successfully.");
                        _logger.LogDebug("Creating an user information.");
                        identResult = await _userManager.AddLoginAsync(user, info);
                        if (identResult.Succeeded)
                        {
                            _logger.LogDebug("Signing in.");
                            await _signInManager.SignInAsync(user, false);
                            _logger.LogInformation("Login successfully added.");
                            _logger.LogTrace("End facebook response.");
                            return RedirectToAction("Index", "Home");
                        } // End logged in successfully
                    } // End created successfully

                    _logger.LogError(identResult.Errors.First().Description.ToString());
                    TempData["Exception"] = @"Swal.fire({
                                                title: 'The " + user.Email + @" is already registered.',
                                                text: 'Would you like to try login instead?',
                                                icon: 'warning',
                                                showCancelButton: false,
                                                confirmButtonColor: '#3085d6',
                                                confirmButtonText: 'Login'
                                            }).then((res) => {
                                                console.log('Confirmed');
                                                window.location.href = '" + $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}/Identity/Account/Login"
                                                + "'});";
                    _logger.LogTrace("End facebook response.");
                    return View();
                } // End signed in successfully
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message.ToString());
                TempData["Exception"] = @"Swal.fire({ icon: 'error', title: 'Error !', text: `" + e.Message.Replace("\\", "/") + @"`, showConfirmButton: true });";
                _logger.LogTrace("End facebook response.");
                return Redirect($"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}/Identity/Account/Logout");
            } // End try catch
        } // End FacebookResponse

        /*
         * Name: MicrosoftLogin
         * Author: Wannapa Srijermtong
         * Description: Getting properties of authentication.
         */
        [AllowAnonymous]
        public IActionResult MicrosoftLogin()
        {
            try
            {
                _logger.LogTrace("Start microsoft login.");
                string redirectUrl = Url.Action("MicrosoftResponse", "Account");
                _logger.LogDebug("Getting properties from Microsoft.");
                var properties = _signInManager.ConfigureExternalAuthenticationProperties("Microsoft", redirectUrl);
                _logger.LogTrace("End microsoft login.");
                return new ChallengeResult("Microsoft", properties);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message.ToString());
                TempData["Exception"] = @"Swal.fire({ icon: 'error', title: 'Error !', text: `" + e.Message.Replace("\\", "/") + @"`, showConfirmButton: true });";
                _logger.LogTrace("End microsoft login.");
                return Redirect($"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}/Identity/Account/Logout");
            } // End try catch
        } // End MicrosoftLogin

        /*
         * Name: MicrosoftResponse
         * Author: Wannapa Srijermtong
         * Description: Creating an account or login to UMS.
         */
        [AllowAnonymous]
        public async Task<IActionResult> MicrosoftResponse()
        {
            try
            {
                _logger.LogTrace("Start microsoft response.");
                ViewData["URL"] = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";
                ViewData["URLHOME"] = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}/Home";

                _logger.LogDebug("Getting external login info.");
                ExternalLoginInfo info = await _signInManager.GetExternalLoginInfoAsync();
                _logger.LogDebug("Get external login.");
                if (info == null) throw new Exception("User information not found.");

                _logger.LogDebug("Getting External login and sign in.");
                var result = await _signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, true);
                _logger.LogTrace("Creating user information .");
                string[] userInfo = {
                    info.Principal.FindFirst(ClaimTypes.Name).Value,
                    info.Principal.FindFirst(ClaimTypes.Email).Value,
                    info.Principal.FindFirst(ClaimTypes.GivenName).Value,
                    info.Principal.FindFirst(ClaimTypes.Surname).Value
                }; // Create new user info

                if (result.Succeeded)
                {
                    var IsActive = _manageUserController.GetStatusUser(info.Principal.FindFirst(ClaimTypes.Email).Value);
                    if (IsActive.Value.ToString() == "ACTIVE")
                    {
                        _logger.LogInformation("Login successfully.");
                        _logger.LogTrace("End microsoft response.");
                        return RedirectToAction("Index", "Home");
                    }
                    else if (IsActive.Value.ToString() == "INACTIVE")
                    {
                        _logger.LogInformation("This user is not active.");
                        _logger.LogDebug("Getting user by email.");
                        ApplicationUser user = await _userManager.FindByEmailAsync(info.Principal.FindFirst(ClaimTypes.Email).Value);
                        if (user == null) throw new Exception("Calling a method on a null object reference.");
                        _manageUserController.deleteUser(user.Id); // Toggle user status
                        _logger.LogInformation("Change status inactive to active user.");
                        _logger.LogTrace("End microsoft response.");
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        throw new Exception("Cannot find this user.");
                    } // End check user is inactive
                } // If you had an account
                else
                {
                    _logger.LogTrace("Creating an application user.");
                    ApplicationUser user = new ApplicationUser
                    {
                        Email = info.Principal.FindFirst(ClaimTypes.Email).Value,
                        UserName = info.Principal.FindFirst(ClaimTypes.Email).Value,
                        acc_Firstname = info.Principal.FindFirst(ClaimTypes.GivenName).Value,
                        acc_Lastname = info.Principal.FindFirst(ClaimTypes.Surname).Value,
                        acc_IsActive = 'Y'
                    }; // Create new user

                    _logger.LogDebug("Creating user information.");
                    IdentityResult identResult = await _userManager.CreateAsync(user);
                    if (identResult.Succeeded)
                    {
                        _logger.LogDebug("Finding user by email.");
                        ApplicationUser userId = await _userManager.FindByEmailAsync(user.Email);
                        await _userManager.AddToRoleAsync(userId, "User");
                        _logger.LogInformation("User created successfully.");
                        _logger.LogDebug("Creating an user information.");
                        identResult = await _userManager.AddLoginAsync(user, info);
                        if (identResult.Succeeded)
                        {
                            _logger.LogDebug("Singing in.");
                            await _signInManager.SignInAsync(user, false);
                            _logger.LogInformation("Login successfully added.");
                            _logger.LogTrace("End microsoft response.");
                            return RedirectToAction("Index", "Home");
                        } // End logged in successfully
                    } // End created successfully

                    _logger.LogError(identResult.Errors.First().Description.ToString());
                    TempData["Exception"] = @"Swal.fire({
                                                title: 'The " + user.Email + @" is already registered.',
                                                text: 'Would you like to try login instead?',
                                                icon: 'warning',
                                                showCancelButton: false,
                                                confirmButtonColor: '#3085d6',
                                                confirmButtonText: 'Login'
                                            }).then((res) => {
                                                console.log('Confirmed');
                                                window.location.href = '" + $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}/Identity/Account/Login"
                                                + "'});";
                    _logger.LogTrace("End microsoft response.");
                    return View();
                } // End signed in successfully
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message.ToString());
                TempData["Exception"] = @"Swal.fire({ icon: 'error', title: 'Error !', text: `" + e.Message.Replace("\\", "/") + @"`, showConfirmButton: true });";
                _logger.LogTrace("End microsoft response.");
                return Redirect($"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}/Identity/Account/Logout");
            } // End try catch
        } // End MicrosoftResponse
    } // End AccountController
}
