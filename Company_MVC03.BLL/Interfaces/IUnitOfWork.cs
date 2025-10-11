using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company_MVC03.BLL.Interfaces
{
    public interface IUnitOfWork : IAsyncDisposable
    {
        IEmployeeRepository EmployeeRepository { get; }
        IDepartmentRepository DepartmentRepository { get; }

        Task<int> CompleteAsync();

    }
}
