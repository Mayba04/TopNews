using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    }
}
