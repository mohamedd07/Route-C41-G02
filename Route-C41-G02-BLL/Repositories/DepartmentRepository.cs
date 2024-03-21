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
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public DepartmentRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;          //new ApplicationDbContext(new Microsoft.EntityFrameworkCore.DbContextOptions<ApplicationDbContext>)
        }

        public int Add(Department department)
        {
            _dbContext.Departments.Add(department);
            return _dbContext.SaveChanges();
        }

        public int Delete(Department department)
        {
            _dbContext.Departments.Remove(department);
            return _dbContext.SaveChanges();
        }

        public Department Get(int id)
        {
            //var department = _dbContext.Departments.Local.Where(D=>D.Id == id).FirstOrDefault();

            //if (department == null)
            //    department = _dbContext.Departments.Where(D=>D.Id == id).FirstOrDefault();

            //return department;

            return _dbContext.Departments.Find(id); // Local then Global
            //return _dbContext.Find<Department>(id);
        }

        public IEnumerable<Department> GetAll()
        {
            return _dbContext.Departments.AsNoTracking().ToList();
        }

        public int Update(Department department)
        {
            _dbContext.Departments.Update(department);
            return _dbContext.SaveChanges();
        }
    }
}
