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
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public EmployeeRepository(ApplicationDbContext dbContext) // Ask CLR For Creating Object From ApplicationDbContext
        {
            _dbContext = dbContext;
        }
        public int Add(Employee employee)
        {
            _dbContext.Employees.Add(employee);
            return _dbContext.SaveChanges();
        }

        public int Delete(Employee employee)
        {
            _dbContext.Employees.Remove(employee);
            return _dbContext.SaveChanges();
        }

        public Employee Get(int id)
        {
            return _dbContext.Find<Employee>(id); // EF Core 3.1 Feture
        }

        public IEnumerable<Employee> GetAll()
        {
            return _dbContext.Employees.AsNoTracking().ToList();
        }

        public int Update(Employee employee)
        {
            _dbContext.Employees.Update(employee);
            return _dbContext.SaveChanges();

        }
    }
}
