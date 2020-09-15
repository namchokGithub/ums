using System;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using static UMS.Controllers.ManageUserController;
using UMS.Models;
using System.Threading;

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
         * Parametor: log(Logs)
         * Description: For Logs monitor
         */
        public IActionResult Index()
        {
            try
            {
                _logger.LogTrace("Start Index.");
                var UserId = User.FindFirstValue(ClaimTypes.NameIdentifier); // Find id
                _logger.LogTrace("Find ID from first value.");
                ViewData["UserId"] = UserId ?? throw new Exception("The user ID not found !."); // Set Data to view

                int numofrow = 100; // Get top log form database
                string sqlGetallLog = @$"Exec dbo.ums_Get_all_log {numofrow}"; // Set sql text for get data
                
                _logger.LogDebug($"Getting top {numofrow} from all logs.");
                var item = _logsContext.Logs.FromSqlRaw(sqlGetallLog).ToList<Logs>();
                ViewData["Logs"] = item ?? throw new Exception("Calling a method on a null object reference."); // Set result to view and check null value
                ViewData["INFO"] = @$"toastr.info('Select lasted logs.');"; // Message for result query
                _logger.LogTrace("End Index.");
                return View();
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message.ToString());
                TempData["Exception"] = @"Swal.fire({ icon: 'error', title: 'Error !', text: `" + e.Message + @"`, showConfirmButton: true })"; // Message to html view
                _logger.LogTrace("End Index.");
                return View();
            } // End try catch
        } // End Index

        /*
         * Name: SearchLogs
         * Parametor: [POST] dateInput(string), message(string)
         * Description: For Logs monitor
         */
        public JsonResult Search(string messageInput, string dateInput)
        {
            try {
                _logger.LogTrace("Start searching logs.");
                string sqlGetLog;
                if ((dateInput == null && messageInput == null) || (dateInput == "" && messageInput == "")) throw new Exception("Please input information for searching."); // End if param both is null 
                _logger.LogDebug("Input Date Input: " + ((dateInput != null && dateInput != "") ? dateInput : "-"));
                _logger.LogDebug("Input Message: " + ((messageInput != null && messageInput != "") ? messageInput : "-"));
                if (dateInput!=null || dateInput != "")
                {
                    _logger.LogTrace("Set dete from dateIinput.");
                    DateTime dateInputStart = Convert.ToDateTime(dateInput.Substring(0, (dateInput.IndexOf("-"))).ToString());
                    DateTime dateInputEnd = Convert.ToDateTime(dateInput.Substring((dateInput.IndexOf("-")) + 1).ToString()); // Set date for query
                    sqlGetLog = @$"Exec dbo.ums_Search_log '{dateInputStart}', '{dateInputEnd}', '{messageInput}'";
                }
                else
                {
                    sqlGetLog = @$"Exec dbo.ums_Search_log '', '', '{messageInput}'";
                } // End if date input not null
                _logger.LogDebug($"Getting log by {(dateInput ?? "")}{(messageInput==null?"":" or "+messageInput)}.");
                var item = _logsContext.Logs.FromSqlRaw(sqlGetLog).ToList<Logs>();
                if(item == null) throw new Exception("Calling a method on a null object reference.");
                _logger.LogTrace("End searching logs.");
                return new JsonResult(item); // Return object JSON
            }
            catch (Exception e)
            {
                _logger.LogError("Error: " + e.Message.ToString());
                string message = @"Swal.fire({ icon: 'error', title: 'Error !', text: `" + e.Message + @"`, showConfirmButton: true })"; // Message to html view
                var er = new objectJSON
                {
                    condition = "error",
                    messages = message,
                    text = e.Message
                }; // Object for set alert
                _logger.LogTrace("End search logs.");
                return new JsonResult(er);
            } // End try catch
        } // End searchLogs
    } // End Logs
}
