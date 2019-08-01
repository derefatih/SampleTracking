using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ProjectManagement.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectManagement.Data.Concrete.EFCore
{
    public class PMContext:IdentityDbContext<ApplicationUser>
    {
        public PMContext(DbContextOptions<PMContext> options)
            :base(options)
        {

        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Sample> Samples { get; set; }
    }
}
