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
    // [Authorize(Roles = "Admin")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IEmailSender _emailSender;
        private readonly UserManager<ApplicationUser> _userManager;

        public HomeController(IEmailSender emailSender, ILogger<HomeController> logger, UserManager<ApplicationUser> userManager)
        {
            _emailSender = emailSender;
            _logger = logger;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            // Set default data
            TempData["UpdateResult"] = null;

            var UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            Console.WriteLine(User.IsInRole("Admin"));

            var user = await _userManager.FindByIdAsync(UserId);
            var roles = await _userManager.GetRolesAsync(user);

            ViewData["UserRole"] = roles.ToString();
            ViewData["UserId"] = UserId;
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
