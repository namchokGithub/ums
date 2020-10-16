using UMS.Models;
using System.Threading.Tasks;
using UMS.Areas.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using System;
using UMS.Data;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

/*
 * Name: IndexModel.cs (extend: PageModel)
 * Author: Idenity system
 * Description: The page for manage account sites.
 */

namespace UMS.Areas.Identity.Pages.Account.Manage
{
    public partial class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly AuthDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        /*
         * Name: IndexModel
         * Parameter: logger(ILogger<AccessDeniedModel>)
         */
        public IndexModel(
            AuthDbContext context,
            ILogger<IndexModel> logger,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            _logger = logger;
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
        } // End constructor

        [BindProperty]
        public Management Input { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            try
            {
                _logger.LogTrace("Start index manamgement.");
                _logger.LogTrace("Finding user ID.");
                ViewData["UserId"] = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? throw new Exception("The user ID not found !.");  // Get user ID
                _logger.LogDebug("Getting all active users.");
                ViewData["User"] = await GetAllAsync() ?? throw new Exception("Calling a method on a null object reference."); // Send data to view Index.cshtml
                await _context.DisposeAsync();
                _logger.LogTrace("End index manamgement.");
                return Page();
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message.ToString());
                TempData["nullException"] = @"Swal.fire({ icon: 'error', title: 'Error !', text: `" + e.Message.Replace("`", "'").Replace("\\", "/") + @"`, showConfirmButton: true });";
                _logger.LogTrace("End index manamgement.");
                return Page();
            } // End try catch
        } // End On get Async

        /*
         * Name: GetAllAsync
         * Author: Namchok Singhachai
         * Description: The getting list of all users.
         */
        public async Task<List<Management>> GetAllAsync()
        {
            return await _context.Management.FromSqlRaw("EXEC [dbo].ums_Get_all_active_user").ToListAsync<Management>();
        } // End GetAllAsync
    } // End index
}