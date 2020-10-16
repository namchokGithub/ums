using Microsoft.Extensions.Logging;
using UMS.Models;
using Microsoft.EntityFrameworkCore;

/*
 * Namespace: ~/Data/ManagementContext.cs
 * Author: Namchok Singhachai
 * Description: Context for management model
 */

namespace UMS.Data
{
    public class ManagementContext : DbContext
    {
        private readonly ILogger<ManagementContext> _logger;

        public ManagementContext(DbContextOptions<ManagementContext> options, ILogger<ManagementContext> logger)
            : base(options)
        {
            _logger = logger;
            _logger.LogDebug("Start Account Context.");
        } // End Constructor

        public DbSet<Management> Management { get; set; }
    } // End ManagementContext
}