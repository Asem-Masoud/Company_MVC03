using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Company_MVC03.BLL.Interfaces;
using Company_MVC03.DAL.Data.Contexts;
using Company_MVC03.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace Company_MVC03.BLL.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly CompanyDbContext _Context;
        public GenericRepository(CompanyDbContext Context)
        {
            _Context = Context;
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            if (typeof(T) == typeof(Employee))
            {
                return (IEnumerable<T>)await _Context.Employees.Include(E => E.Department).ToListAsync();
            }
            return await _Context.Set<T>().ToListAsync();
        }

        public async Task<T?> GetAsync(int id)
        {
            if (typeof(T) == typeof(Employee))
            {
                return await _Context.Employees.Include(E => E.Department).FirstOrDefaultAsync(E => E.Id == id) as T;
            }
            return _Context.Set<T>().Find(id);
        }

        public async Task AddAsync(T model)
        {
            await _Context.Set<T>().AddAsync(model);
            //return _Context.SaveChanges();

        }


        public void Update(T model)
        {
            _Context.Set<T>().Update(model);
            // return _Context.SaveChanges();
        }

        public void Delete(T model)
        {
            _Context.Set<T>().Remove(model);
            //return _Context.SaveChanges();
        }


    }
}
