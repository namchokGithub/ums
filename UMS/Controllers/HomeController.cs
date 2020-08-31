﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using EmailService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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
        private readonly ILogger<HomeController> _logger;
        private readonly IEmailSender _emailSender;

        public HomeController(IEmailSender emailSender, ILogger<HomeController> logger)
        {
            _emailSender = emailSender;
            _logger = logger;
        }

        /*
        * Name: Get Id
        * Author: Wannapa
        * Description: Get User Id
        */
        public IActionResult Index()
        {
            var UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            ViewData["UserId"] = UserId;
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        /*
         * Name: mail
         * Author: Namchok
         * Description: test send Email
         */
        //public void mail()
        //{
        //    var message = new Message(
        //            new string[] { "usermanagement2020@mail.com" }, // add email to send
        //            "So hungy",
        //            "This message is the content from MHEE"
        //        );

        //    _emailSender.SendEmail(message);
        //}

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
