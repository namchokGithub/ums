using System;
using System.Text;
using EmailService;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;
using User_Management_System.Data;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.AspNetCore.Authorization;
using User_Management_System.Controllers;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using User_Management_System.Areas.Identity.Data;

/*
 * Name: ForgotPasswordModel.cs (Extend: PageModel)
 * Author: Idenity system
 * Descriptions: Sending an email for resetting a password.
 */

namespace User_Management_System.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class ForgotPasswordModel : PageModel
    {
        private readonly IEmailSender _emailSender;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<ForgotPasswordModel> _logger;
        private readonly UserManager<ApplicationUser> _userManager;
        /*
         * Name: ForgotPasswordModel
         * Parameter: userManager(UserManager<ApplicationUser>), context(AuthDbContext), emailSender(IEmailSender), logger(ILogger<ForgotPasswordModel>)
         */
        public ForgotPasswordModel(UserManager<ApplicationUser> userManager,
            AuthDbContext context,
            IEmailSender emailSender,
            ILogger<ForgotPasswordModel> logger)
        {
            _logger = logger;
            _emailSender = emailSender;
            _userManager = userManager;
            _unitOfWork = new UnitOfWork(context);
            _logger.LogDebug("Start forgot password model.");
        } // End contructor

        [BindProperty]
        public InputModel Input { get; set; } // For input value

        /*
         * Name: InputModel
         * Description: for input email and ser expession
         */
        public class InputModel
        {
            [Required]
            [RegularExpression(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$"
                , ErrorMessage = "The Email is not valid.")]
            public string Email { get; set; }
        } // End InputModel

        /*
         * Name: OnPostAsync
         * Description: Sending email for set password.
         */
        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                _logger.LogTrace("Start forgot password on post.");
                if (ModelState.IsValid)
                {
                    var user = await _userManager.FindByEmailAsync(Input.Email); // Find user
                    _logger.LogDebug("Finding user.");
                    if (user == null)
                    {
                        _logger.LogWarning("The user was not found.");
                        TempData["Exception"] = @"Swal.fire({ icon: 'error', title: 'Error !', text: 'The user was not found.'});";
                        return Page();
                    } // End check user is null

                    var appUser = await _unitOfWork.Account.GetByIDAsync(user.Id);
                    if (appUser.acc_TypeAccoutname.ToString().ToLower() != "Email".ToLower())
                    {
                        _logger.LogWarning("This user can not change password (Social Account).");
                        TempData["Exception"] = @"Swal.fire({ icon: 'warning', title: 'Can not change password!', text: 'This email is login with social media'})"; // เป็น Social media ไม่สามารถเปลี่ยน Password ได้
                        return Page();
                    } // End checking type email user

                    _logger.LogDebug("Generating code.");
                    var code = await _userManager.GeneratePasswordResetTokenAsync(user); // Genarating token for this user
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));

                    var callbackUrl = Url.Page(
                        "/Account/ResetPassword",
                        pageHandler: null,
                        values: new { area = "Identity", code },
                        protocol: Request.Scheme); // Craeting call back url

                    var message = new Message(
                        new string[] { Input.Email },
                            "Reset Password",
                            @$"
                            <h2>Hello!, {Input.Email} </h2>
                            <br>
                            We got a request to reset your UMS password.
                            <br>
                            You can change your password by clicking the link 
                            <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'asp-route-Email='{Input.Email}'>
                                <b>Change password</b>
                            </a>.
                            <br>
                            If you ignore this message, your password won't be changed.
                            <br><br><br>
                            <hr>
                            Best regards,
                            <br>
                            User Management System.
                          "
                        ); // End craete message 
                    _logger.LogTrace("Sending email.");
                    await _emailSender.SendEmailAsync(message);

                    _logger.LogTrace("End forgot password on post.");
                    return RedirectToPage("./ForgotPasswordConfirmation");
                }
                else
                {
                    TempData["Exception"] = @"Swal.fire({ icon: 'warning', title: 'Warning !', text: `Modelstate is invalid.`, showConfirmButton: true });";
                    _logger.LogWarning("End forgot password on post.");
                    _logger.LogTrace("End forgot password on post.");
                    return Page();
                } // End checking model state
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message.ToString());
                TempData["Exception"] = @"Swal.fire({ icon: 'error', title: 'Error !', text: `" + e.Message.Replace("`", "'").Replace("\\", "/") + @"`, showConfirmButton: true });";
                _logger.LogTrace("End forgot password on post.");
                return Page();
            } // End try catch
        } // End OnPostAsync
    } // End ForgotPasswordModel
}