using CrimeMgmnt.Data;
using CrimeMgmnt.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// NOTE: add the following Nuget Packages for implementing OWIN Security
//      Microsoft.AspNetCore.Identity.EntityFrameworkCore
//      Microsoft.AspNetCore.Identity.UI     ---- This will put the login and all other registering credentials to your controller for authentication.
//      Microsoft.AspNetCore.Diagnostics.EntityFrameworkCore
//NOTE: add the Nuget Package "Swashbuckle.AspNetCore"
//      to enable Swagger Documentation Generation for OpenAPI documentation.

// Add the assembly attribute, to ensure that the Swagger generates the complete API Documentation.
[assembly: ApiConventionType(typeof(DefaultApiConventions))]


namespace CrimeMgmnt
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            // NOTE: This should be the FIRST service registered in the ConfigureServices() method.
            // Register Entity Framework Core Servies to use SQL Server
            // Register the ApplicationDbContext as a Service that can be used using Dependency Injection (DI)
            services.AddDbContext<ApplicationDbContext>((options) =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("MyDefaultConnectionString"));
            });

            // Register the OWIN Identity Middleware
            // to use the default IdentityUser and IdentityRole profiles
            // and store the data in the ApplicationDbContext
            services
                .AddIdentity<IdentityUser, IdentityRole>(options =>
                {
                    options.SignIn.RequireConfirmedAccount = true;
                    options.Password.RequiredLength = 8;
                })
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();  // added to resolve the IEmailService related configuration error!;

            //Register the ASP.net Razor Pages Middleware
            services.AddRazorPages()
                .AddRazorPagesOptions(options =>
                {
                    options.Conventions.AuthorizeAreaFolder("Identity", "/Account/Manage");
                    options.Conventions.AuthorizeAreaPage("Identity", "/Account/Logout");
                });

            // Configure the Application Cookie options
            services
                .ConfigureApplicationCookie(options =>
                {
                    options.LoginPath = "/Identity/Account/Login";
                    options.LogoutPath = "/Identity/Account/Logout";
                    options.AccessDeniedPath = "/Identity/Account/AccessDenied";
                    options.ExpireTimeSpan = TimeSpan.FromMinutes(20);      // Default Session Cookie expiry is 20 minutes
                    options.SlidingExpiration = true;
                    options.Cookie.HttpOnly = true;
                    options.Cookie.Name = "MyAuthCookie";
                });
            ;

            // Register the MVC Middleware - NEEDED for Swagger Documentation Middleware 
            // - NEEDED for the API support (if applicable)
            services.AddMvc();

            // Register the Swagger Documentation Generation Middleware Service
            // URL: https://localhost:xxxx/swagger
            services.AddSwaggerGen(config =>
            {
                config.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Crime Mngmt",
                    Description = "Cyber Crime Management System - API version 1"
                });
            });

            //Register the EmailSender Service for Dependancy Injection
            services.AddSingleton<IEmailSender,MyEmailSenderService>();
        }
       
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(
            IApplicationBuilder app,
            IWebHostEnvironment env,
            RoleManager<IdentityRole> roleManager,
            UserManager<IdentityUser> userManager)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                // Add the Swagger Middleware
                app.UseSwagger();

                // Add the Swagger Documentation Generation Middleware
                app.UseSwaggerUI(config =>
                {
                    config.SwaggerEndpoint("/swagger/v1/swagger.json", "Crime Mngmt API v1");
                });
            }
            else
            {
                app.UseExceptionHandler("/Error");
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
                endpoints.MapRazorPages();

                // Register the ASP.NET Routes for Areas
                endpoints.MapControllerRoute(
                    name: "areas",
                    pattern: "{area}/{controller}/{action=Index}/{id?}");

                // Register the ASP.NET Routes for the MVC Controllers
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });

            // Seed the Database with the System required Roles & User profiles
            ApplicationDbContextSeed.SeedIdentityRolesAsync(roleManager).Wait();
            ApplicationDbContextSeed.SeedIdentityUserAsync(userManager).Wait();
        }
    }
}
