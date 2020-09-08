using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using UMS.Areas.Identity.Data;
using static UMS.Areas.Identity.Pages.Account.LoginModel;

/*
 * Name: HomeController.cs
 * Namespace: Controllers
 * Author: Wannapa
 */

namespace UMS.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            TempData["Exception"] = null;
            _userManager = userManager;
            _signInManager = signInManager;
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
                if (ModelState.IsValid)
                {
                    ApplicationUser appUser = await _userManager.FindByEmailAsync(login.Email);
                    if (appUser != null)
                    {
                        await _signInManager.SignOutAsync();
                        Microsoft.AspNetCore.Identity.SignInResult result = await _signInManager.PasswordSignInAsync(appUser, login.Password, false, false);
                        if (result.Succeeded)
                            return Redirect(login.ReturnUrl ?? "/");
                    }
                    Console.WriteLine("Login Failed: Invalid Email or password");
                    ModelState.AddModelError(nameof(login.Email), "Login Failed: Invalid Email or password");
                }
                return View(login);
            } catch (Exception e)
            {
                string message = @"Swal.fire({ icon: 'error', title: 'Error !', text: `" + e.Message + @"`, showConfirmButton: true })";
                TempData["Exception"] = message;
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
                return RedirectToPage("./Login");
            }
            catch (Exception e)
            {
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
                string redirectUrl = Url.Action("GoogleResponse", "Account");
                var properties = _signInManager.ConfigureExternalAuthenticationProperties("Google", redirectUrl);
                return new ChallengeResult("Google", properties);
            }
            catch (Exception e)
            {
                string message = @"Swal.fire({ icon: 'error', title: 'Error !', text: `" + e.Message + @"`, showConfirmButton: true })";
                TempData["Exception"] = message;
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
                ExternalLoginInfo info = await _signInManager.GetExternalLoginInfoAsync();

                if (info == null)
                    return RedirectToAction(nameof(InputModel));

                var result = await _signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, false);
                string[] userInfo = {
                    info.Principal.FindFirst(ClaimTypes.Name).Value,
                    info.Principal.FindFirst(ClaimTypes.Email).Value,
                    info.Principal.FindFirst(ClaimTypes.GivenName).Value,
                    info.Principal.FindFirst(ClaimTypes.Surname).Value
                }; // Craete user info

                if (result.Succeeded) // If had account
                    return RedirectToAction("Index", "Home");
                else
                {
                    ApplicationUser user = new ApplicationUser
                    {
                        Email = info.Principal.FindFirst(ClaimTypes.Email).Value,
                        UserName = info.Principal.FindFirst(ClaimTypes.Email).Value,
                        acc_Firstname = info.Principal.FindFirst(ClaimTypes.GivenName).Value,
                        acc_Lastname = info.Principal.FindFirst(ClaimTypes.Surname).Value,
                        acc_IsActive = 'Y'
                    }; // create new user

                    IdentityResult identResult = await _userManager.CreateAsync(user);
                    if (identResult.Succeeded)
                    {
                        identResult = await _userManager.AddLoginAsync(user, info);
                        if (identResult.Succeeded)
                        {
                            await _signInManager.SignInAsync(user, false);
                            return RedirectToAction("Index", "Home");
                        }
                    } // If success add login info
                    return AccessDenied();
                } // End if signed in success
            }
            catch (Exception e)
            {
                string message = @"Swal.fire({ icon: 'error', title: 'Error !', text: `" + e.Message + @"`, showConfirmButton: true })";
                TempData["Exception"] = message;
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
                string redirectUrl = Url.Action("FacebookResponse", "Account");
                var properties = _signInManager.ConfigureExternalAuthenticationProperties("Facebook", redirectUrl);
                return new ChallengeResult("Facebook", properties);
            }
            catch (Exception e)
            {
                string message = @"Swal.fire({ icon: 'error', title: 'Error !', text: `" + e.Message + @"`, showConfirmButton: true })";
                TempData["Exception"] = message;
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
                ExternalLoginInfo info = await _signInManager.GetExternalLoginInfoAsync();
                if (info == null)
                    return RedirectToAction(nameof(InputModel));

                var result = await _signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, false);

                string[] userInfo = {
                    info.Principal.FindFirst(ClaimTypes.Name).Value,
                    info.Principal.FindFirst(ClaimTypes.Email).Value,
                    info.Principal.FindFirst(ClaimTypes.GivenName).Value,
                    info.Principal.FindFirst(ClaimTypes.Surname).Value
                }; // Crate new user info

                if (result.Succeeded)
                    return RedirectToAction("Index", "Home");
                else
                {
                    ApplicationUser user = new ApplicationUser
                    {
                        Email = info.Principal.FindFirst(ClaimTypes.Email).Value,
                        UserName = info.Principal.FindFirst(ClaimTypes.Email).Value,
                        acc_Firstname = info.Principal.FindFirst(ClaimTypes.GivenName).Value,
                        acc_Lastname = info.Principal.FindFirst(ClaimTypes.Surname).Value,
                        acc_IsActive = 'Y'
                    }; // Create new user

                    IdentityResult identResult = await _userManager.CreateAsync(user);
                    if (identResult.Succeeded)
                    {
                        identResult = await _userManager.AddLoginAsync(user, info);
                        if (identResult.Succeeded)
                        {
                            await _signInManager.SignInAsync(user, false);
                            return RedirectToAction("Index", "Home");
                        }
                    } // create and add login info
                    return AccessDenied();
                } // End if signed in success
            }
            catch (Exception e)
            {
                string message = @"Swal.fire({ icon: 'error', title: 'Error !', text: `" + e.Message + @"`, showConfirmButton: true })";
                TempData["Exception"] = message;
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
                string redirectUrl = Url.Action("MicrosoftResponse", "Account");
                var properties = _signInManager.ConfigureExternalAuthenticationProperties("Microsoft", redirectUrl);
                return new ChallengeResult("Microsoft", properties);
            }
            catch (Exception e)
            {
                string message = @"Swal.fire({ icon: 'error', title: 'Error !', text: `" + e.Message + @"`, showConfirmButton: true })";
                TempData["Exception"] = message;
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
                ExternalLoginInfo info = await _signInManager.GetExternalLoginInfoAsync();
                if (info == null)
                    return RedirectToAction(nameof(InputModel));

                var result = await _signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, false);
                string[] userInfo = {
                    info.Principal.FindFirst(ClaimTypes.Name).Value,
                    info.Principal.FindFirst(ClaimTypes.Email).Value,
                    info.Principal.FindFirst(ClaimTypes.GivenName).Value,
                    info.Principal.FindFirst(ClaimTypes.Surname).Value
                }; // Create new user info

                if (result.Succeeded)
                    return RedirectToAction("Index", "Home");
                else
                {
                    ApplicationUser user = new ApplicationUser
                    {
                        Email = info.Principal.FindFirst(ClaimTypes.Email).Value,
                        UserName = info.Principal.FindFirst(ClaimTypes.Email).Value,
                        acc_Firstname = info.Principal.FindFirst(ClaimTypes.GivenName).Value,
                        acc_Lastname = info.Principal.FindFirst(ClaimTypes.Surname).Value,
                        acc_IsActive = 'Y'
                    }; // Create new user

                    IdentityResult identResult = await _userManager.CreateAsync(user);
                    if (identResult.Succeeded)
                    {
                        identResult = await _userManager.AddLoginAsync(user, info);
                        if (identResult.Succeeded)
                        {
                            await _signInManager.SignInAsync(user, false);
                            return RedirectToAction("Index", "Home");
                        }
                    } // Crate user and add login info
                    return AccessDenied();
                } // End if signed in success
            }
            catch (Exception e)
            {
                string message = @"Swal.fire({ icon: 'error', title: 'Error !', text: `" + e.Message + @"`, showConfirmButton: true })";
                TempData["Exception"] = message;
                return RedirectToPage("./Login");
            } // End try catch
        } // End MicrosoftResponse
    } // End AccountController
}
