using System;
using System.Reflection;
using AutoMapper;
using CoreTemplate.ApplicationCore;
using CoreTemplate.ApplicationCore.Helpers;
using CoreTemplate.ApplicationCore.Identity;
using CoreTemplate.ApplicationCore.Interfaces;
using CoreTemplate.Infrastructure.Data;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

// ReSharper disable All

namespace CoreTemplate
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
           
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection"),
                    b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));
            
            services.AddScoped<IApplicationDbContext>(provider => provider.GetService<ApplicationDbContext>());

            //services.AddDefaultIdentity<ApplicationUser>()
            //    .AddRoles<ApplicationRole>()
            //    .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddIdentity<ApplicationUser, ApplicationRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders()
                .AddDefaultUI();

            //services.AddIdentity<ApplicationUser, ApplicationRole>(/*options => options.SignIn.RequireConfirmedAccount = true*/)
            //    .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddControllersWithViews()
                .AddFluentValidation(opt => opt.RegisterValidatorsFromAssemblyContaining<IApplicationDbContext>());

            services.AddRazorPages();

            services.ConfigureApplicationCookie(options =>
            {
                options.AccessDeniedPath = "/Identity/Account/AccessDenied";
                options.Cookie.Name = "CoreTemplate_applicationCookie";
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromDays(1);
                options.LoginPath = "/Identity/Account/Login";
                // ReturnUrlParameter requires 
                //using Microsoft.AspNetCore.Authentication.Cookies;
                options.ReturnUrlParameter = CookieAuthenticationDefaults.ReturnUrlParameter;
                options.SlidingExpiration = true;
            });

            //services.Configure<ApplicationOptions>(Configuration.GetSection("ApplicationOptions"));

            services.AddTransient<IPersonService, PersonService>();
            services.AddTransient<IUsernameHelper, UsernameHelper>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddScoped(typeof(IRepository<>), typeof(EfCoreRepository<>));

            services.AddApplicatinCore();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
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
        }
    }
}
