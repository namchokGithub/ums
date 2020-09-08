using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

/*
 * Name: AccessDeniedModel.cs
 * Namespace: UMS.Areas.Identity.Pages.Account
 * Author: Idenity system
 */

namespace UMS.Areas.Identity.Pages.Account
{
    public class AccessDeniedModel : PageModel
    {
        /*
         * Name: OnGet
         * Parameter: none
         * Description: none
         */
        public void OnGet()
        {

        } // End OnGet

        /*
         * Name: OnPost
         * Parameter: none
         * Description: none
         */
        public IActionResult OnPost()
        {
            return LocalRedirect("/");
        } // End OnPost
    } // End AccessDeniedModel
}

