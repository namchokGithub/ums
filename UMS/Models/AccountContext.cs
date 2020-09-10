using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

/*
 * Namespace: ~/Models/AccountContext.cs
 * Author: Namchok Singhachai
 * Description: Context for Account model
 */

namespace UMS.Models
{
    public class AccountContext : DbContext
    {
        private readonly ILogger<AccountContext> _logger;

        public AccountContext(DbContextOptions<AccountContext> options, ILogger<AccountContext> logger)
            : base(options)
        {
            _logger = logger;
            _logger.LogDebug("Account Context.");
        } // End Constructor

        public DbSet<Account> Account { get; set; }
    } // End AccountContext
}
