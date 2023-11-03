using Microsoft.AspNetCore.Builder;
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

namespace TopNews.Infrastructure.Initializers
{
    public class UsersAndRolesInitializers
    {
        public static async Task SeedUserAndRole(IApplicationBuilder applicationBuilder)
        {
            using(var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<AppDbContext>();
                UserManager<AppUser> userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
                context.Database.Migrate();
                if (userManager.FindByEmailAsync("admi@gmail.com").Result == null) 
                {
                    AppUser admin = new AppUser()
                    {
                        FirstName = "John",
                        LastName = "Connor",
                        UserName = "admi@gmail.com",
                        Email = "admi@gmail.com",
                        EmailConfirmed = true,
                        PhoneNumber = "+xx(xxx)xxx-xx-xx",
                        PhoneNumberConfirmed = true,
                    };

                    context.Roles.AddRangeAsync(
                    new IdentityRole()
                    {
                        Name = "Administrator",
                        NormalizedName = "ADMINISTRATOR"
                    });

                  
                    await context.SaveChangesAsync();

                    IdentityResult adminResult = userManager.CreateAsync(admin, "Qwerty-1").Result;
                    if (adminResult.Succeeded)
                    {
                        userManager.AddToRoleAsync(admin, "Administrator").Wait();
                    }

                }

            }
        }
    }
}
