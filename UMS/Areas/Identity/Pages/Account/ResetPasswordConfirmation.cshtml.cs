using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

/*
 * Name: ResetPasswordConfirmationModel.cs
 * Namespace: UMS.Areas.Identity.Pages.Account
 * Author: Idenity system
 */

namespace UMS.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class ResetPasswordConfirmationModel : PageModel
    {
        /*
         * Name: OnGet
         * Parameter: none
         */
        public void OnGet()
        {
        } // End OnGet
    } // End ResetPasswordConfirmationModel
}
