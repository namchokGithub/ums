using UMS.Models;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;

/*
 * Namespace: ~/Models/AccountContext.cs
 * Author: Namchok Singhachai
 * Description: Context for Account model
 */

namespace UMS.Data
{
    public class AccountContext : DbContext
    {
        private readonly ILogger<AccountContext> _logger;

        public AccountContext(DbContextOptions<AccountContext> options, ILogger<AccountContext> logger)
            : base(options)
        {
            _logger = logger;
            _logger.LogDebug("Start Account Context.");
        } // End Constructor

        public DbSet<Account> Account
        {
            get; set;
        }
    } // End AccountContext
}