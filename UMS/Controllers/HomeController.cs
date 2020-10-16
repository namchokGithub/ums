using System;
using System.Diagnostics;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using User_Management_System.Models;
using Microsoft.AspNetCore.Authorization;

/*
 * Name: HomeController.cs
 * Author: Namchok Singhachai
 * Description: The controller manages a home page.
 */

namespace User_Management_System.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        /*
         * Name: HomeController
         * Parameter: logger(ILogger<HomeController>)
         * Author: Wannapa Srijermtong
         */
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            _logger.LogTrace("Start home controller.");
        } // End HomeController

        /*
         * Name: Index
         * Author: Wannapa Srijermtong
         * Description: The first page of UMS.
         */
        public IActionResult Index()
        {
            try
            {
                _logger.LogTrace("Start home index.");
                _logger.LogInformation($"Welcome, {User.Identity.Name}.");
                _logger.LogTrace("Finding a user ID.");
                ViewData["UserId"] = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? throw new Exception("The user ID not found !.");
                _logger.LogTrace("End home index.");
                return View();
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message.ToString());
                TempData["nullException"] = @"Swal.fire({ icon: 'error', title: 'Error !', text: `" + e.Message.Replace("\\", "/").Replace("`", "'") + @"`, showConfirmButton: true });";
                _logger.LogTrace("End home index.");
                return View();
            } // End try catch
        } // End Index

        /*
         * Name: Error
         * Author: System
         * Description: Creating a new error model page.
         */
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            ViewData["URLLOGOUT"] = $"{this.Request.Scheme}://{this.Request.Host}/Identity/Account/Logout";
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        } // End Error
    } // End Home controller
}