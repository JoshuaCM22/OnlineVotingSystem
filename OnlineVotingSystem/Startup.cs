using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OnlineVotingSystem.Models.BusinessLogicLayer;
using OnlineVotingSystem.Models.DataAccessLayer;
using OnlineVotingSystem.Models.DatabaseModels;
using OnlineVotingSystem.Models.Interfaces;
using OnlineVotingSystem.SignalR.Hubs;
using System;

namespace OnlineVotingSystem
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
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));


            services.AddScoped<IAccountRepository, AccountRepository>();
            services.AddScoped<IAccountService, AccountService>();

            services.AddScoped<IAdminRepository, AdminRepository>();
            services.AddScoped<IAdminService, AdminService>();

            services.AddScoped<IVoterRepository, VoterRepository>();
            services.AddScoped<IVoterService, VoterService>();

            services.AddScoped<IResultRepository, ResultRepository>();
            services.AddScoped<IResultService, ResultService>();

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
          .AddCookie(options =>
          {
              options.LoginPath = "/account/login";
              options.AccessDeniedPath = "/access-denied";
              options.ExpireTimeSpan = TimeSpan.FromDays(3);
          });


            services.AddControllersWithViews();

            // Add SignalR Service
            services.AddSignalR();

            // Add session services
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30); // Set session timeout
                options.Cookie.HttpOnly = true; // Security: Prevents JavaScript access
                options.Cookie.IsEssential = true; // Ensure session is always stored
            });

            services.AddDistributedMemoryCache(); // Required for session storage
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();


            app.UseStatusCodePagesWithReExecute("/page-not-found"); // Handles 404 errors
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            // Enable Session Middleware
            app.UseSession();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Account}/{action=Login}/{id?}");

                // Map SignalR Hub
                endpoints.MapHub<ElectionResultHub>("/electionResultHub");
            });
        }
    }
}
