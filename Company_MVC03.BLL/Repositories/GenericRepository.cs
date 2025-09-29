using Company_MVC03.BLL.Interfaces;
using Company_MVC03.DAL.Data.Contexts;
using Company_MVC03.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company_MVC03.BLL.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly CompanyDbContext _Context;
        public GenericRepository(CompanyDbContext Context)
        {
            _Context = Context;
        }

        public IEnumerable<T> GetAll()
        {
            if (typeof(T) == typeof(Employee))
            {
                return (IEnumerable<T>)_Context.Employees.Include(E => E.Department).ToList();
            }
            return _Context.Set<T>().ToList();
        }

        public T? Get(int id)
        {
            if (typeof(T) == typeof(Employee))
            {
                return _Context.Employees.Include(E => E.Department).FirstOrDefault(E => E.Id == id) as T;
            }
            return _Context.Set<T>().Find(id);
        }

        public int Add(T model)
        {
            _Context.Set<T>().Add(model);
            return _Context.SaveChanges();

        }


        public int Update(T model)
        {
            _Context.Set<T>().Update(model);
            return _Context.SaveChanges();
        }

        public int Delete(T model)
        {
            _Context.Set<T>().Remove(model);
            return _Context.SaveChanges();
        }


    }
}
