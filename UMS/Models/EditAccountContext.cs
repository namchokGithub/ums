using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

/*
 * Namespace: ~/Models/EditAccountContext.cs
 * Author: Namchok Singhachai
 * Description: Context for EditAccount model
 */

namespace UMS.Models
{
    public class EditAccountContext : DbContext
    {
        private readonly ILogger<EditAccountContext> _logger;

        public EditAccountContext(DbContextOptions<EditAccountContext> options, ILogger<EditAccountContext> logger)
            : base(options)
        {
            _logger = logger;
            _logger.LogDebug("Edit Account Context.");
        } // End Constructor

        public DbSet<EditAccount> EditAccount { get; set; }
    } // End EditAccountContext
}
