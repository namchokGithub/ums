using System;
using UMS.Data;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;
using static UMS.Controllers.ManageUserController;

/*
 * Name: UMS.Controllers.LogsController
 * Author: Namchok Singhachai
 * Description: The controller manages log monitor page.
 */

namespace UMS.Controllers
{
    [Authorize(Roles = "Admin")]
    public class LogsController : Controller
    {
        private readonly ILogger<LogsController> _logger;
        private readonly IUnitOfWork _unitOfWork;
        /*
         * Name: LogsController
         * Parametor: logger(ILogger<LogsController>), authDbContext(AuthDbContext)
         */
        public LogsController(ILogger<LogsController> logger, AuthDbContext authDbContext)
        {
            _logger = logger;
            _unitOfWork = new UnitOfWork(authDbContext);
            _logger.LogTrace("Start logs controller.");
        } // End Constructor

        /*
         * Name: Deconstruct
         * Description: The deconstructor of logs controller.
         */
        public void Deconstruct()
        {
            _unitOfWork.Dispose();
            _logger.LogTrace("End logs controller.");
        } // End Deconstructor

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
                ViewData["Logs"] = await _unitOfWork.Logs.GetAllAsync(100) ?? throw new Exception("Calling a method on a null object reference."); // Set result to view and check null value
                ViewData["INFO"] = @$"toastr.info('A show of last logs.');"; // Message for result query
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
                _logger.LogDebug($"Getting log by {(dateInput ?? "")}{(messageInput == null ? "" : messageInput!=null&&dateInput==null? messageInput : " or " + messageInput)}.");
                _logger.LogTrace("End searching a logs.");
                return new JsonResult(await _unitOfWork.Logs.SearchAsync(messageInput, dateInput)); // Return object JSON
            }
            catch (Exception e)
            {
                _logger.LogError("Error: " + e.Message.ToString());
                _logger.LogTrace("End searching a logs.");
                return new JsonResult(new objectJSON
                {
                    condition = "error",
                    messages = @"Swal.fire({ icon: 'error', title: 'ERROR!', text: `" + e.Message.Replace("\\", "/").Replace("`", "'") + @"`, showConfirmButton: true });",
                    text = e.Message
                });
            } // End try catch
        } // End searchLogs
    } // End Logs
}