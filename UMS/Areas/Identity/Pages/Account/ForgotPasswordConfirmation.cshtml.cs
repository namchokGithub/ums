using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

/*
 * Name: ForgotPasswordConfirmation.cs
 * Namespace: UMS.Areas.Identity.Pages.Account
 * Author: Idenity system
 */

namespace UMS.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class ForgotPasswordConfirmation : PageModel
    {
        /*
         * Name: OnGet
         * Parameter: none
         */
        public void OnGet() {} // End OnGet

        /*
         * Name: OnPost
         * Parameter: none
         * Description: return to home page
         */
        public IActionResult OnPost()
        {
            return RedirectToPage("Login");
        } // End OnPost
    } // End ForgotPasswordConfirmation
}
