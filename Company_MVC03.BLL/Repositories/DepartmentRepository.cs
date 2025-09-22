using Company_MVC03.BLL.Interfaces;
using Company_MVC03.DAL.Data.Contexts;
using Company_MVC03.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// IN Dependances -> Add Project Reference -> Company_MVC03.DAL
namespace Company_MVC03.BLL.Repositories
{
    // V06

    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly CompanyDbContext _context; // Null

        public DepartmentRepository()
        {
            _context = new CompanyDbContext();
        }

        public IEnumerable<Department> GetAll()
        {
            /*
            using CompanyDbContext context = new CompanyDbContext();
            return context.Departments.ToList();
            */
            return _context.Departments.ToList();
        }

        public Department? Get(int id)
        {
            /*
            using CompanyDbContext context = new CompanyDbContext();
            return context.Departments.Find(id);
            */
            return _context.Departments.Find(id);

        }

        public int Add(Department model)
        {
            /*
            using CompanyDbContext context = new CompanyDbContext();
            context.Departments.Add(model);
            */
            _context.Departments.Add(model);
            return _context.SaveChanges();
        }

        public int Update(Department model)
        {
            /*
             using CompanyDbContext context = new CompanyDbContext();
            context.Departments.Update(model);
            */
            _context.Departments.Update(model);
            return _context.SaveChanges();
        }

        public int Delete(Department model)
        {
            /*
            using CompanyDbContext context = new CompanyDbContext();
            context.Departments.Remove(model);
            */
            _context.Departments.Remove(model);
            return _context.SaveChanges();
        }

    }
}
