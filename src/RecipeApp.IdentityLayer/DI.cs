using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using System;

namespace RecipeApp.IdentityLayer
{
    public static class DI
    {
        public static IServiceCollection AddAppIdentity(this IServiceCollection services, IConfiguration appConfig)
        {
            services.AddDbContext<AppIdentityDbContext>(options =>
            {
                options
                    .EnableSensitiveDataLogging()
                    .UseLazyLoadingProxies()
                    .UseSqlite(appConfig.GetConnectionString("DefaultConnection"));
            });
            services
            .AddIdentity<IdentityUser, IdentityRole>(opt =>
            {
                //lax the sign up/in requirements for dev
                opt.Password.RequireNonAlphanumeric = false;
                opt.Password.RequireDigit = false;
                opt.Password.RequireLowercase = false;
                opt.Password.RequireUppercase = false;

                opt.SignIn.RequireConfirmedAccount = false;
            })
            .AddEntityFrameworkStores<AppIdentityDbContext>()
            .AddDefaultTokenProviders();

            services.ConfigureApplicationCookie(options =>
            {
                // Cookie settings
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(5);

                options.LoginPath = "/Identity/Account/Login";
                options.AccessDeniedPath = "/Identity/Account/AccessDenied";
                options.SlidingExpiration = true;
            });

            return services;
        }
    }
}