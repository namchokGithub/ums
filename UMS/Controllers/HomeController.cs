using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using EmailService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using UMS.Areas.Identity.Data;
using UMS.Models;

/*
 * Name: HomeController.cs
 * Namespace: Controllers
 * Author: Namchok
 */

namespace UMS.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        // Variable for manager
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<ApplicationUser> _userManager;

        /*
         * Name: HomeController(Controller)
         * Parameter: emailSender(IEmailSender), logger(ILogger<HomeController>), userManager(UserManager<ApplicationUser)
         * Author: Wannapa
         * Description: First page of UMS
         */
        public HomeController(ILogger<HomeController> logger, UserManager<ApplicationUser> userManager)
        {
            try
            {
                _logger = logger;
                _logger.LogTrace("NLog injected into HomeController.");
                _userManager = userManager;
                _logger.LogTrace("User manager injected into HomeController.");
                _logger.LogTrace("Start HomeController Constructor.");
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message.ToString());
                _logger.LogTrace("End HomeController Constructor.");
            }// End try catch
        } // End HomeController

        /*
         * Name: Index
         * Parameter: none
         * Author: Wannapa
         * Description: First page of UMS
         */
        public IActionResult Index()
        {
            try
            {
                _logger.LogTrace("Start Index.");
                _logger.LogInformation($"Welcome {User.Identity.Name} to UMS.");
                // Console.WriteLine(User.IsInRole("Admin")); // Check Role of users
                TempData["UpdateResult"] = null;
                // Get ID of user
                var UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                _logger.LogTrace("Find first value from user.");
                if (UserId == null) throw new Exception("The user ID not found !.");
                // Set Data to view
                ViewData["UserId"] = UserId;
                _logger.LogTrace("End Index.");
                return View();
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message.ToString());
                string message = @"Swal.fire({ icon: 'error', title: 'Error !', text: `" + e.Message + @"`, showConfirmButton: true })";
                TempData["nullException"] = message;
                _logger.LogTrace("End Index.");
                return View();
            } // End try catch
        } // End Index

        /*
         * Name: Error
         * Parameter: none
         * Author: System
         * Description: create new model error
         */
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        } // End Error
    } // End Home controller
}
