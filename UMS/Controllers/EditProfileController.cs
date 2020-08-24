using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

/*
 * Name: EditProfileController.cs
 * Namespace: Controllers
 * Author: Namchok
 */

namespace UMS.Controllers
{
    public class EditProfileController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
