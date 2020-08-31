using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        private readonly EditProfileContext _editprofileContext;
        public EditProfileController(EditProfileContext editprofileContext)
        {
            _editprofileContext = editprofileContext;
        }
        public IActionResult Index(string Id)
        {
            // SQL text for exextut procedure
            string sqltext = $"EXEC [dbo].get_User '{Id}'";

            // Query data from "dbo.EditProfile" and Convert to List<EditProfile>
            var user = _editprofileContext.EditProfile.FromSqlRaw(sqltext).ToList<EditProfile>().FirstOrDefault();

            // Send data to view Index.cshtml
            ViewData["User"] = user;
            return View();
        }
    }
}
