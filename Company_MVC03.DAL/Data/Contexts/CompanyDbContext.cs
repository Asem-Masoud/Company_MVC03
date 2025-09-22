using Company_MVC03.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Company_MVC03.DAL.Data.Contexts
{
    internal class CompanyDbContext : DbContext
    {
        // V05
        override protected void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server= PC_2001\\SQLEXPRESS; Database=CompanyDB03;Trusted_Connection=True;TrustServerCertificate=True;");
        }

        public DbSet<Department> Departments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(modelBuilder);
        }

        public CompanyDbContext() : base()
        {

        }
    }

}
