using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

/*
 * Name: ExamplePage.cs
 * Author: Namchok Singhachai
 * Description: The controller page for authentication.
 */

namespace UMS.Controllers
{
    public class ExamplePage : Controller
    {
        private readonly ILogger<ExamplePage> _logger;
        /*
         * Name: AccountController
         * Parameter: logger(ILogger<ExamplePage>)
         */
        public ExamplePage()
        {
            _logger.LogInformation("Start example page controller.");
        } // End consturcter
        /*
         * Name: Index
         * Author: Namchok Singhachai
         */
        public IActionResult Index()
        {
            return View();
        } // End Index
    } // End ExamplePage
}
