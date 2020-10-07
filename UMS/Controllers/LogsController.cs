using System;
using UMS.Data;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;
using static UMS.Controllers.ManageUserController;

/*
 * Name: UMS.Controllers.LogsController
 * Author: Namchok Singhachai
 * Description: Logs monitor controller
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
         * Description: Constructor
         */
        public LogsController(ILogger<LogsController> logger, AuthDbContext authDbContext)
        {
            try
            {
                _logger = logger;
                _unitOfWork = new UnitOfWork(authDbContext);
                _logger.LogTrace("Start Logs Controller.");
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message.ToString());
                _logger.LogTrace("End Logs Controller.");
            }// End try catch
        } // End Constructor

        /*
         * Name: Deconstruct
         * Parametor: none
         * Description: Deconstructor of logs controller.
         */
        public void Deconstruct()
        {
            _unitOfWork.Dispose();
            _logger.LogTrace("End logs controller.");
        } // End Deconstructor

        /*
         * Name: Index
         * Parametor: log(Logs)
         * Description: For Logs monitor
         */
        public IActionResult Index()
        {
            try
            {
                _logger.LogTrace("Start Logs Index.");
                _logger.LogTrace("Finding user ID.");
                ViewData["UserId"] = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? throw new Exception("User ID not found!."); // Get user ID
                _logger.LogDebug($"Getting top 100 from all logs.");
                ViewData["Logs"] = _unitOfWork.Logs.GetAll(100) ?? throw new Exception("Calling a method on a null object reference."); // Set result to view and check null value
                ViewData["INFO"] = @$"toastr.info('Select lasted logs.');"; // Message for result query
                _logger.LogTrace("End Logs Index.");
                return View();
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message.ToString());
                TempData["Exception"] = @"Swal.fire({ icon: 'error', title: 'ERROR!', text: `" + e.Message + @"`, showConfirmButton: true });";
                _logger.LogTrace("End Logs Index.");
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
                _logger.LogTrace("End search logs.");
                return new JsonResult(new objectJSON
                {
                    condition = "error",
                    messages = @"Swal.fire({ icon: 'error', title: 'ERROR!', text: `" + e.Message + @"`, showConfirmButton: true });",
                    text = e.Message
                }); // Message to html view
            } // End try catch
        } // End searchLogs
    } // End Logs
}