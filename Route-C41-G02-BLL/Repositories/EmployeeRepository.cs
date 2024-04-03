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
    public class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(ApplicationDbContext dbContext):base(dbContext)
        {
            
        }
        public IQueryable<Employee> GetEmployeeByAddress(string address)
        {
            return _dbContext.Employees.Where(e => e.Address.ToLower() == address.ToLower()); // Not the best way
        }

        public new async Task<IEnumerable<Employee>> GetAllAsync()
        {
            return await _dbContext.Set<Employee>().Include(E=> E.Department).AsNoTracking().ToListAsync();
        }
        public IQueryable<Employee> SearchByName(string name)
        {
            return _dbContext.Employees.Where(E=> E.Name.ToLower().Contains(name));

        }
    }
}
