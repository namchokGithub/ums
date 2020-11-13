using System;
using UMS.Data;
using EmailService;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;

/*
 * Name : Startup
 * Author: System
 * Description: Setting up the project.
 */

namespace UMS
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
            services.AddAuthentication(options => {
                //options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                //options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
            })
            .AddCookie(options => {
                options.LoginPath = @"/Identity/Account/Login";
            })
            .AddOpenIdConnect(o =>
            {
                o.ClientId = "1655213862";
                o.ClientSecret = "cc6a98410d26fefe7020b4b855bd78c0";
                o.ResponseType = OpenIdConnectResponseType.Code;
                o.GetClaimsFromUserInfoEndpoint = true;
                o.UseTokenLifetime = true;
                o.SaveTokens = true;
                o.Scope.Add("email");
                o.CallbackPath = "/Home";
                o.Configuration = new OpenIdConnectConfiguration
                {
                    Issuer = "https://access.line.me",
                    AuthorizationEndpoint = "https://access.line.me/oauth2/v2.1/authorize?bot_prompt=aggressive&response_type=code",
                    TokenEndpoint = "https://api.line.me/oauth2/v2.1/token"
                };
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(o.ClientSecret)),
                    NameClaimType = "Line",
                    ValidAudience = o.ClientId
                };
            });

            // Repository
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<ILogsRepository, LogsRepository>();
            services.AddScoped<IAccountRepository, AccountRepository>();

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
            services.AddRazorPages().AddRazorRuntimeCompilation();

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