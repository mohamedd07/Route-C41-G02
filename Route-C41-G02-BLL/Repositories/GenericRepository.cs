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
    public class GenericRepository<T> : Interfaces.IGenericRepository<T> where T : ModelBase
    {
        private protected readonly ApplicationDbContext _dbContext;

        public GenericRepository(ApplicationDbContext dbContext) // Ask CLR For Creating Object From ApplicationDbContext
        {
            _dbContext = dbContext;
        }
        public void Add(T entity)
        {
            _dbContext.Set<T>().Add(entity);
            //return _dbContext.SaveChanges();
        }

        public void Delete(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
            //return _dbContext.SaveChanges();
        }

        public async Task<T> GetAsync(int id)
        {
            return await _dbContext.FindAsync<T>(id); // EF Core 3.1 Feture
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbContext.Set<T>().AsNoTracking().ToListAsync();
        }

        public void Update(T entity)
        {
            _dbContext.Set<T>().Update(entity);
            //return _dbContext.SaveChanges();

        }

    }
}
