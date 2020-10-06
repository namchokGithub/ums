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
using UMS.Data;
using System.Collections.Generic;

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
        private readonly IUnitOfWork _unitOfWork;
        /*
         * Name: Logs
         * Parametor: none
         * Description: Constructor
         */
        public LogsController(ILogger<LogsController> logger, LogsContext logsContext, AuthDbContext authDbContext)
        {
            try
            {
                _logger = logger;
                _logsContext = logsContext;
                _unitOfWork = new UnitOfWork(authDbContext);
                _logger.LogTrace("Start LogsController Constructor.");
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message.ToString());
                _logger.LogTrace("End LogsController Constructor.");
            }// End try catch
        } // End Constructor

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
                _logger.LogTrace("Find ID from first value.");
                ViewData["UserId"] = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? throw new Exception("The user ID not found!."); // Get user ID
                _logger.LogDebug($"Getting top 100 from all logs.");
                ViewData["Logs"] = _unitOfWork.Logs.GetAll(100) ?? throw new Exception("Calling a method on a null object reference."); // Set result to view and check null value
                _unitOfWork.Logs.Dispose();
                ViewData["INFO"] = @$"toastr.info('Select lasted logs.');"; // Message for result query
                _logger.LogTrace("End Index.");
                return View();
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message.ToString());
                TempData["Exception"] = @"Swal.fire({ icon: 'error', title: 'Error !', text: `" + e.Message + @"`, showConfirmButton: true });";
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
            try
            {
                _logger.LogTrace("Start searching logs.");
                if ((dateInput == null && messageInput == null) || (dateInput == "" && messageInput == ""))
                    throw new Exception("Please input information for searching."); // End if param both is null 
                _logger.LogDebug("Input Date Input: " + ((dateInput != null && dateInput != "") ? dateInput : "-"));
                _logger.LogDebug("Input Message: " + ((messageInput != null && messageInput != "") ? messageInput : "-"));
                _logger.LogDebug($"Getting log by {(dateInput ?? "")}{(messageInput == null ? "" : " or " + messageInput)}.");
                _logger.LogTrace("End searching logs.");
                return new JsonResult(_unitOfWork.Logs.Search(messageInput, dateInput)); // Return object JSON
            }
            catch (Exception e)
            {
                _logger.LogError("Error: " + e.Message.ToString());
                string message = @"Swal.fire({ icon: 'error', title: 'Error !', text: `" + e.Message + @"`, showConfirmButton: true })";
                var er = new objectJSON
                {
                    condition = "error",
                    messages = message,
                    text = e.Message
                }; // Object for set alert 
                _logger.LogTrace("End search logs.");
                return new JsonResult(er); // Message to html view
            }
            finally
            {
                _unitOfWork.Logs.Dispose();
            } // End try catch
        } // End searchLogs
    } // End Logs
}
