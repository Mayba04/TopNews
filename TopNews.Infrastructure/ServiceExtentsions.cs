using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopNews.Core.Entities.User;
using TopNews.Infrastructure.Context;

namespace TopNews.Infrastructure
{
    public static class ServiceExtentsions
    {
        public static void AddDbCotext(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<AppDbContext>(opt => 
            {
                opt.UseSqlServer(connectionString);
                opt.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            });
        }

        public static void AddInfastructuresServices(this IServiceCollection services)
        {
            services.AddIdentity<AppUser, IdentityRole>(options => 
            {
                options.SignIn.RequireConfirmedEmail = true;
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 6 ;
                options.Password.RequireNonAlphanumeric = true;
                options.User.RequireUniqueEmail = true;
            }).AddEntityFrameworkStores<AppDbContext>().
            AddDefaultTokenProviders();
        }
    }
}
