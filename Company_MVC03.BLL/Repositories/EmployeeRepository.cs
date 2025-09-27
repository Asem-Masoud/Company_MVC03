using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Company_MVC03.BLL.Interfaces;
using Company_MVC03.DAL.Models;
using Company_MVC03.DAL.Data.Contexts;

namespace Company_MVC03.BLL.Repositories
{
    public class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository
    {

        public EmployeeRepository(CompanyDbContext Context) : base(Context) // ASK CLR Create object From CompanyDbContext
        {
            //_Context = Context;
        }


    }
}
