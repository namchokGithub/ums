using System;
using EmailService;
using User_Management_System.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

/*
 * Name : Startup
 * Author: System
 * Description: Setting up the project.
 */

namespace User_Management_System
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        /*
         * Name: Startup
         * Parameter: configuration(IConfiguration)
         */
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        } // End Constructor

        /*
         * Name: ConfigureServices
         * Parameter: services(IServiceCollection)
         * Description: This method gets called by the runtime. Use this method to add services to the container.
         */
        public void ConfigureServices(IServiceCollection services)
        {
            // Repository
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<ILogsRepository, LogsRepository>();
            services.AddScoped<IAccountRepository, AccountRepository>();

            // Set connect database
            services.AddDbContext<ManagementContext>(options =>
                    options.UseSqlServer(
                       Configuration.GetConnectionString("AuthDbContextConnection")));

            // Cookies
            services.ConfigureApplicationCookie(o =>
            {
                o.ExpireTimeSpan = TimeSpan.FromDays(14);
                o.SlidingExpiration = true;
                o.ReturnUrlParameter = "/";
            });

            // Service for send email
            var emailConfig = Configuration
                .GetSection("EmailConfiguration")
                .Get<EmailConfiguration>();
            services.AddSingleton(emailConfig);
            services.AddScoped<IEmailSender, EmailSender>();

            // Controller with view and add razor pages
            services.AddControllersWithViews();
            services.AddRazorPages();

            // Add mvc
            services.AddMvcCore().AddDataAnnotations();

            // Service for Google authentication
            services.AddAuthentication()
            .AddGoogle(googleOptions =>
            {
                googleOptions.ClientId = "168669914421-am1vveumu5aaggtsnmceac4eedpp4nrb.apps.googleusercontent.com";
                googleOptions.ClientSecret = "_Z33IY-6xE8B_DkbAUKWqTFX";
                googleOptions.SignInScheme = IdentityConstants.ExternalScheme;
            });

            // Service for Facebook authentication
            services.AddAuthentication()
            .AddFacebook(facebookOptions =>
            {
                facebookOptions.AppId = "775622693211895";
                facebookOptions.AppSecret = "bac3818b714dd5282277916f3c56f172";
            });

            // Service for Microsoft authentication
            services.AddAuthentication()
            .AddMicrosoftAccount(microsoftOptions =>
            {
                microsoftOptions.ClientId = "ed43a983-e56c-4a2a-a1e8-55b74d56fbc4";
                microsoftOptions.ClientSecret = ".j-k5QvM40k4Mzm1d7PwWBAQv~w42_MV.e";
            });
        } // End ConfigureServices

        /*
         * Name: Configure
         * Parameter: app(IApplicationBuilder), env(IWebHostEnvironment)
         * Description: This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
         */
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        } // End Configure
    } // End Startup
}