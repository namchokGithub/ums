using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UMS.Models;

/*
 * Name: EditProfileController.cs
 * Namespace: Controllers
 * Author: Wannapa
 */

namespace UMS.Controllers
{
    public class EditProfileController : Controller
    {
        private readonly AccountContext _accountContext;
        public EditProfileController(AccountContext accountContext)
        {
            _accountContext = accountContext;
        }
        public IActionResult Index()
        {
            var user = _accountContext.Account.FirstOrDefault<Account>();

            ViewData["User"] = user;
            return View();
        }
    }
}
