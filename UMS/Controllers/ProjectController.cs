using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using UMS.Data;

/*
 * Name: ProjectController.cs
 * Author: Namchok Singhachai
 * Description: The controller manages user.
 */

namespace UMS.Controllers
{
    //[Authorize(Roles = "Admin")]
    public class ProjectController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<ProjectController> _logger;
                /*
         * Name: ManageUserController
         * Parameter: context(AuthDbContext), logger(ILogger<ManageUserController>), signInManager(SignInManager<ApplicationUser>), userManager(UserManager<ApplicationUser>)
         * Author: Namchok Singhachai
         */
        public ProjectController(AuthDbContext context, ILogger<ProjectController> logger)
        {
            _logger = logger;
            _unitOfWork = new UnitOfWork(context);
            _logger.LogInformation("Start project controller.");
        } // End constructor

        public IActionResult Index()
        {
            return View();
        } // End index
    } // End project controller
}
