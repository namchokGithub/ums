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
        private readonly IEmailSender _emailSender;
        private readonly UserManager<ApplicationUser> _userManager;

        /*
         * Name: HomeController(Controller)
         * Parameter: emailSender(IEmailSender), logger(ILogger<HomeController>), userManager(UserManager<ApplicationUser)
         * Author: Wannapa
         * Description: First page of UMS
         */
        public HomeController(IEmailSender emailSender, ILogger<HomeController> logger, UserManager<ApplicationUser> userManager)
        {
            _emailSender = emailSender;
            _logger = logger;
            _userManager = userManager;

            _logger.LogDebug(1, "NLog injected into HomeController");
            _logger.LogDebug(1, "Email injected into HomeController");
            _logger.LogDebug(1, "User manager injected into HomeController");
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
                _logger.LogInformation("Welcome to UMS.");
                // Console.WriteLine(User.IsInRole("Admin")); // Check Role of users
                TempData["UpdateResult"] = null;
                // Get ID of user
                var UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (UserId == null) throw new Exception("The user ID not found !.");

                //var user = await _userManager.FindByIdAsync(UserId);        // Find user
                //var roles = await _userManager.GetRolesAsync(user);         // Get role user
                //var ian = roles[0].ToString();

                // Set Data to view
                ViewData["UserId"] = UserId;
                _logger.LogTrace("Set user ID");
                return View();
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message.ToString());
                _logger.LogCritical(e.Message.ToString());
                string message = @"Swal.fire({ icon: 'error', title: 'Error !', text: `" + e.Message + @"`, showConfirmButton: true })";
                TempData["nullException"] = message;
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
