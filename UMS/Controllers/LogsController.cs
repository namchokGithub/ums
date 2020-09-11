using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

/*
 * Name: UMS.Controllers.LogsController
 * Author: Namchok Singhachai
 * Description: For Logs monitor
 */

namespace UMS.Controllers
{
    [Authorize(Roles = "Admin")]
    public class LogsController : Controller
    {
        private readonly ILogger<LogsController> _logger;

        /*
         * Name: Logs
         * Parametor: none
         * Description: Constructor
         */
        public LogsController(ILogger<LogsController> logger)
        {
            _logger = logger;
            _logger.LogTrace("Start LogsControllerl.");
        } // End Consturctor

        /*
         * Name: Index
         * Parametor: none
         * Description: For Logs monitor
         */
        public IActionResult Index()
        {
            return View();
        } // End Index
    } // End Logs
}
