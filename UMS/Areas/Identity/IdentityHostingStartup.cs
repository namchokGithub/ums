using User_Management_System.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using User_Management_System.Areas.Identity.Data;

/*
 * Name: IdentityHostingStartup (Extend: IHostingStartup)
 * Namespace: ~/Area/Identity
 * Description: Configuration for identity user.
 */

[assembly: HostingStartup(typeof(User_Management_System.Areas.Identity.IdentityHostingStartup))]
namespace User_Management_System.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        /*
         * Name: Configura
         * Parameter: builder(IWebHostBuilder)
         * Description: Configuration for identity user.
         */
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<AuthDbContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("AuthDbContextConnection")));

                services.AddDefaultIdentity<ApplicationUser>(options =>
                {
                    options.SignIn.RequireConfirmedAccount = false;
                    options.Password.RequireDigit = false;
                    options.Password.RequireLowercase = false;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequireUppercase = false;
                })
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<AuthDbContext>();
            });
        } // End Configure
    } // End IdentityHostingStartup
}