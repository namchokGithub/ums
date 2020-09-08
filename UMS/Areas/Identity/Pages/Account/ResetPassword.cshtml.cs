﻿using System;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using UMS.Areas.Identity.Data;

/*
 * Name: ResetPasswordModel.cs
 * Namespace: UMS.Areas.Identity.Pages.Account
 * Author: Idenity system
 */

namespace UMS.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class ResetPasswordModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public ResetPasswordModel(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        [BindProperty]
        public InputModel Input { get; set; }

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
            [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[$@$!%*?&])[A-Za-z0-9$@$!%*?&]+$"
                , ErrorMessage = "The password must contain at least <br> 1 uppercase, 1 lowercase, 1 digit and 1 special character.")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }

            public string Code { get; set; }
        }

        /*
         * Name: OnGet
         * Parameter: code(String)
         * Description: Check code for set password
         */
        public IActionResult OnGet(string code = null)
        {
            try
            {
                if (code == null)
                {
                    Console.WriteLine("A code must be supplied.");
                    throw new Exception("A code must be supplied for password reset.");
                }
                else
                {
                    Input = new InputModel
                    {
                        Code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code))
                    };
                    return Page();
                }
            } catch (Exception e)
            {
                string message = @"Swal.fire({ icon: 'error', title: 'Error !', text: `" + e.Message + @"`, showConfirmButton: true })";
                TempData["Exception"] = message;
                return Page();
            } // End try catch
        } // End OnGet

        /*
         * Name: OnPostAsync
         * Parameter: none
         * Description: Check code for set password
         */
        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    Console.WriteLine("ModelState: false.");
                    return Page();
                }

                var user = await _userManager.FindByEmailAsync(Input.Email);

                if (user == null)
                {
                    // Don't reveal that the user does not exist
                    Console.WriteLine("Not found user!");
                    return RedirectToPage("./Login");
                }

                var result = await _userManager.ResetPasswordAsync(user, Input.Code, Input.Password);

                if (result.Succeeded)
                {
                    return RedirectToPage("./ResetPasswordConfirmation");
                }

                string errorStr = "";
                foreach (var error in result.Errors)
                {
                    errorStr += error.Description + " (" + error.Code + "). ";
                    ModelState.AddModelError(string.Empty, error.Description);
                } // End loop get error

                // Send alert to home pages
                TempData["Exception"] = @"Swal.fire({ icon: 'error', title: 'Error !', text: `" + errorStr + @"`, showConfirmButton: true })";
                return Page();
            }
            catch (Exception e)
            {
                string message = @"Swal.fire({ icon: 'error', title: 'Error !', text: `" + e.Message + @"`, showConfirmButton: true })";
                TempData["Exception"] = message;
                return Page();
            } // End try catch
        } // End OnPostAsync
    } // End ResetPasswordModel
}
