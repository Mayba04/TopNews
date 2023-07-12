using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopNews.Core.Entities.User;

namespace TopNews.Infrastructure.Context
{
    internal class AppDbContext : IdentityDbContext
    {
        public AppDbContext() : base() { }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<AppUser> AppUser { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //using (var serviceScope = _serviceProvider.CreateScope())
            //{
            //    var scopedServices = serviceScope.ServiceProvider;
            //    var context = scopedServices.GetRequiredService<AppDbContext>();
            //    var userManager = scopedServices.GetRequiredService<UserManager<AppUser>>();

            //    if (userManager.FindByEmailAsync("admi@gmail.com").Result == null)
            //    {
            //        AppUser admin = new AppUser()
            //        {
            //            FirstName = "John",
            //            LastName = "Connor",
            //            UserName = "admi@gmail.com",
            //            Email = "admi@gmail.com",
            //            EmailConfirmed = true,
            //            PhoneNumber = "+xx(xxx)xxx-xx-xx",
            //            PhoneNumberConfirmed = true,
            //        };

            //        context.Roles.AddRange(
            //            new IdentityRole()
            //            {
            //                Name = "Administrator",
            //                NormalizedName = "ADMINISTRATOR"
            //            });
            //        context.SaveChanges();

            //        IdentityResult adminResult = userManager.CreateAsync(admin, "Qwerty-1").Result;
            //        if (adminResult.Succeeded)
            //        {
            //            userManager.AddToRoleAsync(admin, "Administrator").Wait();
            //        }
            //    }
            //}
        }

    }
}
