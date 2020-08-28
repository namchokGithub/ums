using Microsoft.EntityFrameworkCore;
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
        public EditAccountContext(DbContextOptions<EditAccountContext> options)
            : base(options)
        {
        }

        public DbSet<EditAccount> EditAccount { get; set; }
    }
}
