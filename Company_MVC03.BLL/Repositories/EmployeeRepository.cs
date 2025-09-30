using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Company_MVC03.BLL.Interfaces;
using Company_MVC03.DAL.Models;
using Company_MVC03.DAL.Data.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Company_MVC03.BLL.Repositories
{
    public class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository
    {
        private readonly CompanyDbContext _Context;

        // ASK CLR Create object From CompanyDbContext
        public EmployeeRepository(CompanyDbContext Context) : base(Context)
        {
            _Context = Context;
        }

        public List<Employee> GetByName(string name)
        {
            return _Context.Employees.Include(E => E.Department).Where(E => E.Name.ToLower().Contains(name.ToLower())).ToList();
        }
    }
}
