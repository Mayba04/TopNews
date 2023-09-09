using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopNews.Core.AutoMapper.Categories;
using TopNews.Core.AutoMapper.Ip;
using TopNews.Core.AutoMapper.Posts;
using TopNews.Core.AutoMapper.User;
using TopNews.Core.Interfaces;
using TopNews.Core.Services;

namespace TopNews.Core
{
    public static class ServiceExtensions
    {
        public static void AddCoreServices(this IServiceCollection services)
        {
            services.AddTransient<UserService>();
            services.AddTransient<EmailServices>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IPostService, PostService>();
            services.AddScoped<IDashdoardAccessService, DashboardAccsessService>();
        }

        public static void AddMapping(this IServiceCollection services) 
        {
            services.AddAutoMapper(typeof(AutoMapperUserProfile));
            services.AddAutoMapper(typeof(AutoMapperCategoryProfile));
            services.AddAutoMapper(typeof(AutoMapperPostProfile));
            services.AddAutoMapper(typeof(AutoMapperDashboardAccsessProfile));
        }
    }
}
