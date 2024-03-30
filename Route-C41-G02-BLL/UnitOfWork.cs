using Route_C41_G02_BLL.Interfaces;
using Route_C41_G02_BLL.Repositories;
using Route_C41_G02_DAL.Data;
using Route_C41_G02_DAL.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Route_C41_G02_BLL
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _dbContext;
        //private Dictionary<string, IGenericRepository<ModelBase>> _repositories; 
        private Hashtable _repositories;

        public IEmployeeRepository EmployeeRepository { get; set; } = null;
        public IDepartmentRepository DepartmentRepository { get; set; } = null;

        public UnitOfWork(ApplicationDbContext dbContext)
        {
            _repositories = new Hashtable();
            //EmployeeRepository = new EmployeeRepository(_dbContext);
            //DepartmentRepository = new DepartmentRepository(_dbContext);
            _dbContext = dbContext;
        }
        public int Complete()
        {
            return _dbContext.SaveChanges();
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }

        public Interfaces.IGenericRepository<T> Repository<T>() where T : ModelBase
        {
            var key = typeof(T).Name;
            if (!_repositories.Contains(key))
            {
                var repository = new GenericRepository<T>(_dbContext);
                _repositories.Add(key, repository);
            }

            return _repositories[key] as IGenericRepository<T>;

        }
    }
}
