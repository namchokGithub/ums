using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using UMS.Models;

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
        private readonly LogsContext _logsContext;

        /*
         * Name: Logs
         * Parametor: none
         * Description: Constructor
         */
        public LogsController(ILogger<LogsController> logger, LogsContext logsContext)
        {
            try
            {
                _logger = logger;
                _logsContext = logsContext;
                _logger.LogTrace("Logs Context injected into LogsController.");
                _logger.LogTrace("Start LogsControllerl.");
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message.ToString());
                _logger.LogTrace("End LogsControllerl.");
            }// End try catch
        } // End Consturctor

        /*
         * Name: Index
         * Parametor: none
         * Description: For Logs monitor
         */
        public IActionResult Index()
        {
            try
            {
                _logger.LogTrace("Start Index.");
                TempData["UpdateResult"] = null;
                var UserId = User.FindFirstValue(ClaimTypes.NameIdentifier); // Find id
                _logger.LogTrace("Find id from first value.");
                if (UserId == null) throw new Exception("The user ID not found !.");
                ViewData["UserId"] = UserId; // Set Data to view

                string sqlGetallLog = @"Exec dbo.ums_Get_all_log";
                _logger.LogDebug("Getting top 50 from all logs.");
                var item = _logsContext.Logs.FromSqlRaw(sqlGetallLog).ToList<Logs>();
                ViewData["Logs"] = item;
                _logger.LogTrace("End Index.");
                return View();
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message.ToString());
                string message = @"Swal.fire({ icon: 'error', title: 'Error !', text: `" + e.Message + @"`, showConfirmButton: true })";
                TempData["Exception"] = message;
                _logger.LogTrace("End Index.");
                return View();
            } // End try catch
        } // End Index
    } // End Logs
}
