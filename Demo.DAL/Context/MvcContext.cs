using Demo.DAL.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.DAL.Context
{
   // public class MvcContext : DbContext  
        public class MvcContext : IdentityDbContext<ApplicationUser>
    {
        // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //=> optionsBuilder.UseSqlServer("Server=.;DataBase=MvcAppDb;Trusted_Connection= true;MultipleActiveResultSets=true;");

        public MvcContext(DbContextOptions<MvcContext> options) : base(options)
        {

        }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Employee> Employees { get; set; }

    }
}
