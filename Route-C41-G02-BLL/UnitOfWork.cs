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
        ////private Dictionary<string, IGenericRepository<ModelBase>> _repositories; 
        private Hashtable _repositories;



        //public IEmployeeRepository EmployeeRepository { get; set; } = null;
        //public IDepartmentRepository DepartmentRepository { get; set; } = null;

        public UnitOfWork(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            _repositories = new Hashtable();

            //EmployeeRepository = new EmployeeRepository(_dbContext);
            //DepartmentRepository = new DepartmentRepository(_dbContext);

        }


        public async Task<int> Complete()
        {
            return await _dbContext.SaveChangesAsync();
        }

        public async ValueTask DisposeAsync()
        {
            await _dbContext.DisposeAsync();
        }

        public IGenericRepository<T> Repository<T>() where T : ModelBase
        {
            var key = typeof(T).Name;

            if (!_repositories.ContainsKey(key))
            {
                if (key == nameof(Employee))
                {
                    var repository = new EmployeeRepository(_dbContext);
                    _repositories[key] = repository;
                }
                else
                {
                    var repository = new GenericRepository<T>(_dbContext);
                    _repositories.Add(key, repository);
                }
            }

            return _repositories[key] as IGenericRepository<T>;
        }
    }
}
