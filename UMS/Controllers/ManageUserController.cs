using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UMS.Models;

/*
 * Name: MangeUserController.cs
 * Namespace: Controllers
 * Author: Namchok
 */

namespace UMS.Controllers
{
    public class ManageUserController : Controller
    {
        private readonly AccountContext _accountContext;

        public ManageUserController(AccountContext accountContext)
        {
            _accountContext = accountContext;
        }

        public IActionResult Index()
        {
            // Query data from "dbo.Account" and Convert to List<Account>
            var user = _accountContext.Account.ToList<Account>();
            // Send data to view Index.cshtml
            ViewData["User"] = user;
            return View();
        }
    }
}
