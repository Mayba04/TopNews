using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
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
        public AppDbContext(): base() {  }

        public AppDbContext(DbContextOptions<AppDbContext> options): base(options) { }

        public DbSet<AppUser> AppUser { get; set; }


    }
}
