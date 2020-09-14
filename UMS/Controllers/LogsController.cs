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
using static UMS.Controllers.ManageUserController;

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
        private List<Logs> temp = null;

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
         * Parametor: log(Logs)
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
                if (temp == null)
                {
                    // Get top log form database
                    int numofrow = 100;
                    string sqlGetallLog = @$"Exec dbo.ums_Get_all_log {numofrow}";
                    _logger.LogDebug($"Getting top {numofrow} from all logs.");
                    var item = _logsContext.Logs.FromSqlRaw(sqlGetallLog).ToList<Logs>();
                    temp = null;
                    ViewData["Logs"] = item;
                } else
                {
                    // Get log by searching
                    _logger.LogTrace("Set list of log to view index.");
                    ViewData["Logs"] = temp;
                } // End if param is null
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

        /*
         * Name: SearchLogs
         * Parametor: [POST] dateInput(string), message(string)
         * Description: For Logs monitor
         */
        public JsonResult Search(string messageInput,string dateInput)
        {
            try {
                _logger.LogTrace("Start searching logs.");
                _logger.LogTrace("Getting value from input form.");
                DateTime dateInputStart = Convert.ToDateTime(dateInput.Substring(0, (dateInput.IndexOf("-"))).ToString());
                DateTime dateInputEnd = Convert.ToDateTime(dateInput.Substring((dateInput.IndexOf("-"))+1).ToString());

                _logger.LogDebug("Input dateInput: " + ((dateInput != null|| dateInput != "") ? dateInput : "-"));
                _logger.LogDebug("Input Message: " + ((messageInput != null || messageInput != "") ? messageInput : "-"));
                if((dateInput == null && messageInput == null) || (dateInput == "" && messageInput == ""))
                {
                    _logger.LogWarning("Please input information for searching.");
                    throw new Exception("Please input information for searching.");
                } // End if param both is null 
                string sqlGetLog = @$"Exec dbo.ums_Search_log '{dateInputStart}', '{dateInputEnd}', '{messageInput}'";
                if (messageInput == null | messageInput == "") messageInput = "-";
                _logger.LogDebug($"Getting log by {(dateInput == null?"-":dateInput)} or {messageInput}.");
                var item = _logsContext.Logs.FromSqlRaw(sqlGetLog).ToList<Logs>();
                if (item == null) throw new Exception("Calling a method on a null object reference.");
                _logger.LogTrace("End searching logs.");
                return new JsonResult(item);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message.ToString());
                TempData["Exception"] = @"Swal.fire({ icon: 'error', title: 'Error !', text: `" + e.Message + @"`, showConfirmButton: true })";
                var er = new objectJSON
                {
                    condition = "error",
                    messages = e.Message
                }; // Object for set alert
                _logger.LogTrace("End search logs.");
                return new JsonResult(er);
            } // End try catch
        } // End searchLogs
    } // End Logs
}
