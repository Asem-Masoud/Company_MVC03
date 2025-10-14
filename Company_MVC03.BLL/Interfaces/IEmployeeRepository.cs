using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Company_MVC03.DAL.Models;

namespace Company_MVC03.BLL.Interfaces
{
    public interface IEmployeeRepository : IGenericRepository<Employee>
    {
        //List<Employee> GetByName(string name); // For Search Option
        Task<List<Employee>> GetByNameAsync(string name);
        //IEnumerable<Employee> GetAll();

        //Employee? Get(int id);

        //int Add(Employee employee);

        //int Update(Employee employee);

        //int Delete(Employee employee);


    }
}
