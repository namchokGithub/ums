using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
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
        private readonly ILogger<EditProfileContext> _logger;

        public EditProfileContext(DbContextOptions<EditProfileContext> options, ILogger<EditProfileContext> logger)
            : base(options)
        {
            _logger = logger;
            _logger.LogDebug("Edit Profile Context.");
        } // End Constructor

        public DbSet<EditProfile> EditProfile { get; set; }
    }
}
