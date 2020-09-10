using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

/*
 * Name: AccessDeniedModel.cs
 * Namespace: UMS.Areas.Identity.Pages.Account
 * Author: Idenity system
 */

namespace UMS.Areas.Identity.Pages.Account
{
    public class AccessDeniedModel : PageModel
    {
        private readonly ILogger<AccessDeniedModel> _logger;

        public AccessDeniedModel(ILogger<AccessDeniedModel> logger)
        {
            _logger = logger;
            _logger.LogDebug("Access Denined model.");
        } // End constructor

        /*
         * Name: OnGet
         * Parameter: none
         * Description: none
         */
        public void OnGet() { _logger.LogTrace("Access Denined On get.");  } // End OnGet

        /*
         * Name: OnPost
         * Parameter: none
         * Description: none
         */
        public IActionResult OnPost()
        {
            _logger.LogTrace("Access Denined On post.");
            return LocalRedirect("/");
        } // End OnPost
    } // End AccessDeniedModel
}

