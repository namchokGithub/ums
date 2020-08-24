using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using UMS.Areas.Identity.Data;
using UMS.Models;

namespace UMS.Data
{
    public class AuthDbContext : IdentityDbContext<ApplicationUser>
    {
        public AuthDbContext(DbContextOptions<AuthDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ApplicationUser>(entity =>
                {
                    entity.Property(e => e.Id).HasColumnName("acc_Id");
                    entity.Property(e => e.UserName).HasColumnName("acc_UserName");
                    entity.Property(e => e.NormalizedUserName).HasColumnName("acc_NormalizedUserName");
                    entity.Property(e => e.PasswordHash).HasColumnName("acc_PasswordHash");
                    entity.Property(e => e.SecurityStamp).HasColumnName("acc_SecurityStamp");
                    entity.Property(e => e.ConcurrencyStamp).HasColumnName("acc_ConcurrencyStamp");
                    entity.Property(e => e.Email).HasColumnName("acc_Email");
                    entity.Property(e => e.acc_Firstname).HasColumnName("acc_Firstname");
                    entity.Property(e => e.acc_Lastname).HasColumnName("acc_Lastname");
                    entity.Property(e => e.acc_IsActive).HasColumnName("acc_IsActive");
                    entity.Property(e => e.acc_ro_Id).HasColumnName("acc_ro_Id");
                    entity.Property(e => e.acc_ta_Id).HasColumnName("acc_ta_Id");
                }
            );

            builder.Entity<ApplicationUser>()
                .Ignore(entity => entity.LockoutEnd)
                .Ignore(entity => entity.NormalizedEmail)
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

            builder.Entity<Log>(entity => {
                entity.Property(e => e.log_Id).HasColumnName("log_Id");
                entity.Property(e => e.log_datetime).HasColumnName("log_datetime");
            });

            builder.Entity<LogAccount>(entity => {
                entity.Property(e => e.la_Id).HasColumnName("la_Id");
                entity.Property(e => e.la_log_Id).HasColumnName("la_log_Id");
                entity.Property(e => e.la_acc_Id).HasColumnName("la_acc_Id");
            });
        }
    }
}
