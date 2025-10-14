using Company_MVC03.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Company_MVC03.DAL.Data.Contexts
{
    public class CompanyDbContext : /*DbContext*/ IdentityDbContext<AppUser>
    {
        // V05
        // CLR
        //override protected void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer("Server= PC_2001\\SQLEXPRESS; Database=CompanyDB03;Trusted_Connection=True;TrustServerCertificate=True;");
        //}

        public DbSet<Department> Departments { get; set; }
        public DbSet<Employee> Employees { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(modelBuilder);
        }
        /*
        public CompanyDbContext() : base()
        {

        }
        */
        //V07

        public CompanyDbContext(DbContextOptions<CompanyDbContext> options) : base(options)
        {

        }
    }


}
