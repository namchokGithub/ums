using Microsoft.EntityFrameworkCore;
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
        public AccountContext(DbContextOptions<AccountContext> options)
            : base(options)
        {
        }

        public DbSet<Account> Account { get; set; }
    } // End AccountContext
}
