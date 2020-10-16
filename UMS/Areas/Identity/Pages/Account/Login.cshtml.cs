using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using User_Management_System.Data;
using Microsoft.Extensions.Logging;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Identity;
using User_Management_System.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using User_Management_System.Areas.Identity.Data;

/*
 * Name: LoginModel.cs (Extend: PageModel)
 * Auther: Namchok Singhachai
 * Description : The authentication for login.
 */

namespace User_Management_System.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class LoginModel : PageModel
    {
        private readonly ILogger<LoginModel> _logger;
        private readonly ManageUserController _manageUser;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        /*
         * Name: LoginModel
         * Parameter: context (AuthDbContext), signInManager (SignInManager<ApplicationUser>), logger (ILogger<LoginModel>), 
         *            loggerManageUser (ILogger<ManageUserController>), userManager (UserManager<ApplicationUser>)
         */
        public LoginModel(
            AuthDbContext context, SignInManager<ApplicationUser> signInManager,
            ILogger<LoginModel> logger, ILogger<ManageUserController> loggerManageUser,
            UserManager<ApplicationUser> userManager)
        {
            _logger = logger;
            _userManager = userManager;
            _signInManager = signInManager;
            _manageUser = new ManageUserController(context, loggerManageUser);
            _logger.LogDebug("Start login model.");
        } // End constructor

        [TempData]
        public string ErrorMessage { get; set; }
        [BindProperty]
        public InputModel Input { get; set; }
        public string ReturnUrl { get; set; }
        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        /*
         * Name: InputModel
         * Description: The recording of input.
         */
        public class InputModel
        {
            [Required]
            [RegularExpression(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$"
                , ErrorMessage = "The Email is not valid.")]
            public string Email { get; set; }

            [Required]
            //[DataType(DataType.Password)]
            [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[\:\;\[\]\\\|\/\.\,\'\""\()\{}\<>_#$!%@@^฿*?&\-\+\=])[A-Za-z0-9\:\;\[\]\\\|\/\.\,\'\""\()\{}\<>_#$!%@@^฿*?&\-\+\=]+$"
                , ErrorMessage = "The password must contain at least <br> 1 uppercase, 1 lowercase, 1 digit and 1 special character.")]
            public string Password { get; set; }

            [Display(Name = "Remember me?")]
            public bool RememberMe { get; set; }

            public string ReturnUrl { get; set; }
        } // End InputModel

        /*
         * Name: OnGetAsync
         * Parameter: returnUrl(String)
         * Description: Checking user logged in and getting user information.
         */
        public async Task OnGetAsync(string returnUrl = null)
        {
            try
            {
                _logger.LogTrace("Start login on get.");
                ViewData["URL"] = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";
                if (User.Identity.IsAuthenticated)
                {
                    _logger.LogInformation("User is authenticated.");
                    Response.Redirect($"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}");
                } // Check if logged in
                if (!string.IsNullOrEmpty(ErrorMessage))
                {
                    _logger.LogError(ErrorMessage.ToString());
                    ModelState.AddModelError(string.Empty, ErrorMessage);
                } // check if has error message
                returnUrl ??= Url.Content($"{this.Request.Host}{Request.PathBase}");  // Clear the existing external cookie to ensure a clean login process
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
                TempData["Exception"] = @"Swal.fire({ icon: 'error', title: 'Error !', text: `" + e.Message.Replace("\\", "/").Replace("`", "'") + @"`, showConfirmButton: true })";
                _logger.LogTrace("End login on get.");
            } // End try catch
        } // End OnGetAsync

        /*
         * Name: OnPostAsync
         * Parameter: returnUrl(string)
         * Description: The login to this system.
         */
        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            try
            {
                _logger.LogTrace("Start login on post.");
                returnUrl ??= Url.Content("~/");
                ViewData["URL"] = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";
                if (ModelState.IsValid)
                {
                    _logger.LogTrace("Signing in with password.");
                    var result = await _signInManager.PasswordSignInAsync(Input.Email, Input.Password, Input.RememberMe, lockoutOnFailure: false);
                    if (result.Succeeded)
                    {
                        ApplicationUser user = await _userManager.FindByEmailAsync(Input.Email.ToString());
                        if (user.acc_IsActive == 'N')
                        {
                            await _manageUser.DeleteUser(user.Id);
                            _logger.LogInformation("Change status Inactive to active user.");
                        } // End check status
                        _logger.LogInformation("User logged in successfully.");

                        string nameCookies = StringEncryptor.EncryptString("usermanagementsystem2020", "remembermeums");
                        if (Input.RememberMe)
                        {
                            CookieOptions option = new CookieOptions
                            {
                                Expires = DateTime.Now.AddDays(14),
                                Path = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}/Identity/Account/Login",
                                HttpOnly = true,
                                SameSite = SameSiteMode.Lax
                            };
                            string cookies = StringEncryptor.EncryptString("usermanagementsystem2020", "UMS.Cookies%" + Input.Email.ToString() + "%" + Input.Password.ToString());
                            Response.Cookies.Delete(nameCookies.ToString());
                            HttpContext.Response.Cookies.Append(nameCookies.ToString(), cookies, option);
                            _logger.LogInformation("Adding cookie into the browser.");
                        } // Remember email and password

                        _logger.LogTrace("End login on post.");
                        return LocalRedirect(returnUrl);
                    } // If user logged in successfully
                    else
                    {
                        _logger.LogWarning("Your email or password is not valid.");
                        ModelState.AddModelError(string.Empty, "Your email or password is not valid.");
                        TempData["ExceptionInValid"] = "InValid"; // Send alert to home pages
                        _logger.LogTrace("End login on post.");
                        return Page();
                    } // If Loged out
                } // End if check model state
                _logger.LogTrace("End login on post.");
                return Page();
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message.ToString());
                TempData["Exception"] = @"Swal.fire({ icon: 'error', title: 'Error !', text: `" + e.Message.Replace("\\", "/").Replace("`", "'") + @"`, showConfirmButton: true })";
                _logger.LogTrace("End login on post.");
                return Page();
            } // End try catch
        } // End OnPostAsync

        /*
         * Name: StringEncryptor
         * Auther: Namchok Singhachai
         * Description : The encryption and decryption
         */
        public class StringEncryptor
        {
            /*
             * Name: EncryptString
             * Parameter: key(string), plainText(string)
             * Description: The encryption of string with key.
             */
            public static string EncryptString(string key, string plainText)
            {
                byte[] iv = new byte[16];
                byte[] array;

                using (Aes aes = Aes.Create())
                {
                    aes.Key = Encoding.UTF8.GetBytes(key);
                    aes.IV = iv;
                    ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
                    using MemoryStream memoryStream = new MemoryStream();
                    using CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, encryptor, CryptoStreamMode.Write);
                    using (StreamWriter streamWriter = new StreamWriter((Stream)cryptoStream))
                    {
                        streamWriter.Write(plainText);
                    }
                    array = memoryStream.ToArray();
                }

                return Convert.ToBase64String(array);
            } // End EncryptString

            /*
             * Name: DecryptString
             * Parameter: key(string), cipherText(string)
             * Description: The decryption of string with key.
             */
            public static string DecryptString(string key, string cipherText)
            {
                byte[] iv = new byte[16];
                byte[] buffer = Convert.FromBase64String(cipherText);

                using Aes aes = Aes.Create();
                aes.Key = Encoding.UTF8.GetBytes(key);
                aes.IV = iv;
                ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

                using MemoryStream memoryStream = new MemoryStream(buffer);
                using CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, decryptor, CryptoStreamMode.Read);
                using StreamReader streamReader = new StreamReader((Stream)cryptoStream);
                return streamReader.ReadToEnd();
            } // End DecryptString

            /*
             * Name: DecryptStringToEmail
             * Parameter: cipherText(string)
             * Description: The decryption of sting with key.
             */
            public static string DecryptStringToEmail(string key, string cipherText)
            {
                string text = DecryptString(key, cipherText);
                string t = text.Substring(text.IndexOf('%') + 1);
                string e = t.Substring(0, t.IndexOf('%'));
                return e;
            } // End DecryptStringToEmail

            /*
             * Name: DecryptStringToPass
             * Parameter: cipherText(string)
             * Description: The decryption of sting with key.
             */
            public static string DecryptStringToPass(string key, string cipherText)
            {
                string text = DecryptString(key, cipherText);
                string t = text.Substring(text.IndexOf('%') + 1);
                string p = t.Substring(t.IndexOf('%') + 1);
                return p;
            } // End DecryptStringToPass
        }
    } // End login
}