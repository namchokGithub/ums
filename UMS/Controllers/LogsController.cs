using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using User_Management_System.Data;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;
using static User_Management_System.Controllers.ManageUserController;

/*
 * Name: UMS.Controllers.LogsController
 * Author: Namchok Singhachai
 * Description: The controller manages log monitor page.
 */

namespace User_Management_System.Controllers
{
    [Authorize(Roles = "Admin")]
    public class LogsController : Controller
    {
        private readonly ILogsRepository _logs;
        private readonly ILogger<LogsController> _logger;
        /*
         * Name: LogsController
         * Parametor: logger(ILogger<LogsController>), authDbContext(AuthDbContext)
         */
        public LogsController(ILogger<LogsController> logger, AuthDbContext context)
        {
            _logger = logger;
            _logs = new LogsRepository(context);
            _logger.LogTrace("Start logs controller.");
        } // End Constructor

        /*
         * Name: Index
         * Author: Namchok Singhachai
         * Description: A show of logs.
         */
        public async Task<IActionResult> Index()
        {
            try
            {
                _logger.LogTrace("Start logs index.");
                _logger.LogTrace("Finding user ID.");
                ViewData["UserId"] = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? throw new Exception("The user ID not found!."); // Get user ID
                _logger.LogDebug($"Getting top 100 from all logs.");
                ViewData["Logs"] = await _logs.GetAllAsync(100) ?? throw new Exception("Calling a method on a null object reference."); // Set result to view and check null value
                ViewData["INFO"] = @$"toastr.info('Show the last 100 results.');";
                _logger.LogTrace("End logs index.");
                return View();
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message.ToString());
                TempData["Exception"] = @"Swal.fire({ icon: 'error', title: 'ERROR!', text: `" + e.Message.Replace("\\", "/").Replace("`", "'") + @"`, showConfirmButton: true });";
                _logger.LogTrace("End logs index.");
                return View();
            } // End try catch
        } // End Index

        /*
         * Name: SearchLogs
         * Parametor: [POST] dateInput(string), message(string)
         * Author: Namchok Singhachai
         * Description: Searching a log.
         */
        public async Task<JsonResult> Search(string messageInput, string dateInput)
        {
            try
            {
                _logger.LogTrace("Start searching a logs.");
                if ((dateInput == null && messageInput == null) || (dateInput == "" && messageInput == ""))
                    throw new Exception("Please input information for searching."); // End if param both is null 
                _logger.LogDebug("Input Date Input: " + ((dateInput != null && dateInput != "") ? dateInput : "-"));
                _logger.LogDebug("Input Message: " + ((messageInput != null && messageInput != "") ? messageInput : "-"));
                _logger.LogDebug($"Getting log by {(dateInput ?? "")}{(messageInput == null ? "" : messageInput != null && dateInput == null ? messageInput : " or " + messageInput)}.");
                _logger.LogTrace("End searching a logs.");
                return new JsonResult(await _logs.SearchAsync(messageInput, dateInput)); // Return object JSON
            }
            catch (Exception e)
            {
                _logger.LogError("Error: " + e.Message.ToString());
                _logger.LogTrace("End searching a logs.");
                return new JsonResult(new ObjectJSON
                {
                    condition = "error",
                    messages = @"Swal.fire({ icon: 'error', title: 'ERROR!', text: `" + e.Message.Replace("\\", "/").Replace("`", "'") + @"`, showConfirmButton: true });",
                    text = e.Message.Replace("\\", "/").Replace("`", "'")
                });
            } // End try catch
        } // End searchLogs
    } // End Logs
}