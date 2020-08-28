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
            var user = _accountContext.Account.ToList<Account>();
            
            Console.WriteLine(user);
            
            ViewData["User"] = user;
            return View();
        }
    }
}
