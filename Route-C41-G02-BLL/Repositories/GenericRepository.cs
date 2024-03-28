using Microsoft.EntityFrameworkCore;
using Route_C41_G02_BLL.Interfaces;
using Route_C41_G02_DAL.Data;
using Route_C41_G02_DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Route_C41_G02_BLL.Repositories
{
    public class GenericRepository<T> : Interfaces.GenericRepository<T> where T : ModelBase
    {
        private protected readonly ApplicationDbContext _dbContext;

        public GenericRepository(ApplicationDbContext dbContext) // Ask CLR For Creating Object From ApplicationDbContext
        {
            _dbContext = dbContext;
        }
        public int Add(T entity)
        {
            _dbContext.Set<T>().Add(entity);
            return _dbContext.SaveChanges();
        }

        public int Delete(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
            return _dbContext.SaveChanges();
        }

        public T Get(int id)
        {
            return _dbContext.Find<T>(id); // EF Core 3.1 Feture
        }

        public IEnumerable<T> GetAll()
        {
            if(typeof(T) == typeof(Employee)) // NOT The best Solution
            {
                return (IEnumerable <T>) _dbContext.Employees.Include(E=> E.Department).AsNoTracking().ToList();
            }
            return _dbContext.Set<T>().AsNoTracking().ToList();
        }

        public int Update(T entity)
        {
            _dbContext.Set<T>().Update(entity);
            return _dbContext.SaveChanges();

        }
    }
}
