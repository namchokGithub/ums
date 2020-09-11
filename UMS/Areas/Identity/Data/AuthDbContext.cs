﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using UMS.Areas.Identity.Data;
using UMS.Models;

/*
 * Name: AuthDbContext (Extend: IdentityDbContext<ApplicationUser>)
 * Namespace: ~/Area/Identity/Data
 */

namespace UMS.Data
{
    public class AuthDbContext : IdentityDbContext<ApplicationUser>
    {
        private readonly ILogger<AuthDbContext> _logger;

        public AuthDbContext(DbContextOptions<AuthDbContext> options, ILogger<AuthDbContext> logger)
            : base(options)
        {
            _logger = logger;
            _logger.LogDebug("Start Auth Database Context.");
        } // End contructor

        /*
         * Name: OnModelCreating
         * Parametor: builder(ModelBuilder)
         * Description: For config the modal before creat database
         */
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            
            builder.Entity<ApplicationUser>(entity =>
                {
                    entity.Property(e => e.Id).HasColumnName("acc_Id");
                    entity.Property(e => e.UserName).HasColumnName("acc_User");
                    entity.Property(e => e.NormalizedUserName).HasColumnName("acc_NormalizedUserName");
                    entity.Property(e => e.PasswordHash).HasColumnName("acc_PasswordHash");
                    entity.Property(e => e.SecurityStamp).HasColumnName("acc_SecurityStamp");
                    entity.Property(e => e.ConcurrencyStamp).HasColumnName("acc_ConcurrencyStamp");
                    entity.Property(e => e.Email).HasColumnName("acc_Email");
                    entity.Property(e => e.NormalizedEmail).HasColumnName("acc_NormalizedEmail");
                    entity.Property(e => e.acc_Firstname).HasColumnName("acc_Firstname");
                    entity.Property(e => e.acc_Lastname).HasColumnName("acc_Lastname");
                    entity.Property(e => e.acc_IsActive).HasColumnName("acc_IsActive");
                }
            );

            builder.Entity<ApplicationUser>()
                .Ignore(entity => entity.LockoutEnd)
                .Ignore(entity => entity.LockoutEnabled)
                .Ignore(entity => entity.PhoneNumber)
                .Ignore(entity => entity.EmailConfirmed)
                .Ignore(entity => entity.TwoFactorEnabled)
                .Ignore(entity => entity.AccessFailedCount)
                .Ignore(entity => entity.PhoneNumberConfirmed)
                .ToTable(name: "Account");

            builder.Entity<IdentityRole>().ToTable("Roles");
            builder.Entity<IdentityUserRole<string>>().ToTable("UserRoles");
            builder.Entity<IdentityUserLogin<string>>().ToTable("UserLogins");
            builder.Entity<IdentityUserToken<string>>().ToTable("UserTokens");
            builder.Entity<IdentityUserClaim<string>>().ToTable("UserClaims");
            builder.Entity<IdentityRoleClaim<string>>().ToTable("RoleClaims");

            builder.Entity<Log>().HasNoKey();
            builder.Entity<Log>(entity =>
            {
                entity.Property(e => e.log_datetime).HasColumnName("log_datetime");
                entity.Property(e => e.log_datetime).HasComment("Date time");
                entity.Property(e => e.log_level).HasColumnName("log_level");
                entity.Property(e => e.log_level).HasComment("Level of log");
                entity.Property(e => e.log_logger).HasColumnName("log_logger");
                entity.Property(e => e.log_user_identity).HasColumnName("log_user_identity");
                entity.Property(e => e.log_mvc_action).HasColumnName("log_mvc_action");
                entity.Property(e => e.log_mvc_controller).HasColumnName("log_mvc_controller");
                entity.Property(e => e.log_filename).HasColumnName("log_filename");
                entity.Property(e => e.log_linenumber).HasColumnName("log_linenumber");
                entity.Property(e => e.log_message).HasColumnName("log_message");
                entity.Property(e => e.log_exception).HasColumnName("log_exception");
                entity.Property(e => e.log_exception).HasComment("Exception");
            });
        } // End OnModelCreating
    } // End AuthDbContext
}
