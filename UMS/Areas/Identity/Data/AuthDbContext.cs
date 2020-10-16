using Microsoft.Extensions.Logging;
using User_Management_System.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using User_Management_System.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

/*
 * Name: AuthDbContext (Extend: IdentityDbContext<ApplicationUser>)
 * Namespace: ~/Area/Identity/Data
 * Description: The context of database for application.
 */

namespace User_Management_System.Data
{
    public class AuthDbContext : IdentityDbContext<ApplicationUser>
    {
        private readonly ILogger<AuthDbContext> _logger;
        /*
         * Name: AuthDbContext
         * Parameter: options(DbContextOptions<AuthDbContext>), logger(ILogger<AuthDbContext>)
         */
        public AuthDbContext(DbContextOptions<AuthDbContext> options, ILogger<AuthDbContext> logger)
            : base(options)
        {
            _logger = logger;
            _logger.LogTrace("Start application context.");
        } // End Contructor

        /*
         * Name: OnModelCreating
         * Parametor: builder(ModelBuilder)
         * Description: Configuration for this application context.
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

                entity.Property(e => e.Id).HasComment("User ID");
                entity.Property(e => e.UserName).HasComment("Username");
                entity.Property(e => e.NormalizedUserName).HasComment("Normalized UserName");
                entity.Property(e => e.PasswordHash).HasComment("Password hash");
                entity.Property(e => e.SecurityStamp).HasComment("Security Stamp");
                entity.Property(e => e.ConcurrencyStamp).HasComment("For check edit state");
                entity.Property(e => e.Email).HasComment("User email");
                entity.Property(e => e.NormalizedEmail).HasComment("Normalized user email");
                entity.Property(e => e.acc_Firstname).HasComment("Firstname");
                entity.Property(e => e.acc_Lastname).HasComment("Lastname");
                entity.Property(e => e.acc_IsActive).HasComment("Status of account");
            });

            builder.Entity<ApplicationUser>()
                .Ignore(entity => entity.LockoutEnd)
                .Ignore(entity => entity.LockoutEnabled)
                .Ignore(entity => entity.PhoneNumber)
                .Ignore(entity => entity.EmailConfirmed)
                .Ignore(entity => entity.TwoFactorEnabled)
                .Ignore(entity => entity.AccessFailedCount)
                .Ignore(entity => entity.PhoneNumberConfirmed)
                .ToTable(name: "Account");
            // _logger.LogTrace("Creating applications user models.");

            builder.Entity<IdentityRole>().ToTable("Roles");
            builder.Entity<IdentityUserRole<string>>().ToTable("UserRoles");
            builder.Entity<IdentityUserLogin<string>>().ToTable("UserLogins");
            builder.Entity<IdentityUserToken<string>>().ToTable("UserTokens");
            builder.Entity<IdentityUserClaim<string>>().ToTable("UserClaims");
            builder.Entity<IdentityRoleClaim<string>>().ToTable("RoleClaims");
            // _logger.LogTrace("Creating Identity models.");

            builder.Entity<Logs>(entity =>
            {
                entity.Property(e => e.log_Id).HasColumnName("log_Id");
                entity.Property(e => e.log_datetime).HasColumnName("log_datetime");
                entity.Property(e => e.log_datetime).HasColumnType("datetime");
                entity.Property(e => e.log_level).HasColumnName("log_level");
                entity.Property(e => e.log_logger).HasColumnName("log_logger");
                entity.Property(e => e.log_user_identity).HasColumnName("log_user_identity");
                entity.Property(e => e.log_mvc_action).HasColumnName("log_mvc_action");
                entity.Property(e => e.log_filename).HasColumnName("log_filename");
                entity.Property(e => e.log_linenumber).HasColumnName("log_linenumber");
                entity.Property(e => e.log_message).HasColumnName("log_message");
                entity.Property(e => e.log_exception).HasColumnName("log_exception");
                entity.Property(e => e.log_datetime).HasComment("Date time");
                entity.Property(e => e.log_level).HasComment("Level of log");
                entity.Property(e => e.log_exception).HasComment("Exception");
                entity.Property(e => e.log_logger).HasComment("A computer program to keep track of events.");
            });

            builder.Entity<Logs>()
                .Ignore(e => e.log_date)
                .Ignore(e => e.log_time);
            // _logger.LogTrace("Creating log models.");
            // _logger.LogTrace("End creating on model.");
        } // End OnModelCreating

        public DbSet<Logs> Logs { get; set; } // Set table logs
        public DbSet<Management> Management { get; set; } // Set table logs
    } // End AuthDbContext
}