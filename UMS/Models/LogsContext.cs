using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

/*
 * Namespace: ~/Models/LogsContext.cs
 * Author: Namchok Singhachai
 * Description: Context for Logs model
 */

namespace UMS.Models
{
    public class LogsContext : DbContext
    {
        private readonly ILogger<LogsContext> _logger;

        public LogsContext(DbContextOptions<LogsContext> options, ILogger<LogsContext> logger)
            : base(options)
        {
            _logger = logger;
            _logger.LogTrace("Start Edit Profile Context.");
        } // End Constructor

        public DbSet<Logs> Logs { get; set; }
    } // End Log context
}
