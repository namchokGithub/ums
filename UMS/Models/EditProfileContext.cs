using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

/*
 * Namespace: ~/Models/EditProfileContext.cs
 * Author: Wannapa Srijermtong
 * Description: Context for Account model
 */
namespace UMS.Models
{
    public class EditProfileContext : DbContext
    {
        public EditProfileContext(DbContextOptions<EditProfileContext> options)
           : base(options)
        {
        }

        public DbSet<EditProfile> EditProfile { get; set; }
    }
}
