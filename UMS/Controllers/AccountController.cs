using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Facebook;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using UMS.Areas.Identity.Data;
using static UMS.Areas.Identity.Pages.Account.LoginModel;

namespace UMS.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        public IActionResult InputModel(string returnUrl)
        {
            InputModel login = new InputModel();
            login.ReturnUrl = returnUrl;
            return View(login);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> InputModel(InputModel login)
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
                ModelState.AddModelError(nameof(login.Email), "Login Failed: Invalid Email or password");
            }
            return View(login);
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        public IActionResult AccessDenied()
        {
            return View();
        }

        [AllowAnonymous]
        public IActionResult GoogleLogin()
        {
            string redirectUrl = Url.Action("GoogleResponse", "Account");
            var properties = _signInManager.ConfigureExternalAuthenticationProperties("Google", redirectUrl);
            return new ChallengeResult("Google", properties);
        }

        [AllowAnonymous]
        public async Task<IActionResult> GoogleResponse()
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
            };
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
                };

                IdentityResult identResult = await _userManager.CreateAsync(user);
                if (identResult.Succeeded)
                {
                    identResult = await _userManager.AddLoginAsync(user, info);
                    if (identResult.Succeeded)
                    {
                        await _signInManager.SignInAsync(user, false);
                        return RedirectToAction("Index", "Home");
                    }
                }
                return AccessDenied();
            }
        }

        [AllowAnonymous]
        public IActionResult FacebookLogin()
        {
            string redirectUrl = Url.Action("FacebookResponse", "Account");
            var properties = _signInManager.ConfigureExternalAuthenticationProperties("Facebook", redirectUrl);
            return new ChallengeResult("Facebook", properties);
        }

        [AllowAnonymous]
        public async Task<IActionResult> FacebookResponse()
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
            };
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
                };

                IdentityResult identResult = await _userManager.CreateAsync(user);
                if (identResult.Succeeded)
                {
                    identResult = await _userManager.AddLoginAsync(user, info);
                    if (identResult.Succeeded)
                    {
                        await _signInManager.SignInAsync(user, false);
                        return RedirectToAction("Index", "Home");
                    }
                }
                return AccessDenied();
            }
        }

        [AllowAnonymous]
        public IActionResult MicrosoftLogin()
        {
            string redirectUrl = Url.Action("MicrosoftResponse", "Account");
            var properties = _signInManager.ConfigureExternalAuthenticationProperties("Microsoft", redirectUrl);
            return new ChallengeResult("Microsoft", properties);
        }

        [AllowAnonymous]
        public async Task<IActionResult> MicrosoftResponse()
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
            };
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
                };

                IdentityResult identResult = await _userManager.CreateAsync(user);
                if (identResult.Succeeded)
                {
                    identResult = await _userManager.AddLoginAsync(user, info);
                    if (identResult.Succeeded)
                    {
                        await _signInManager.SignInAsync(user, false);
                        return RedirectToAction("Index", "Home");
                    }
                }
                return AccessDenied();
            }
        }
    }
}
