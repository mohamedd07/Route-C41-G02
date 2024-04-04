using Route_C41_G02_DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Route_C41_G02_BLL.Interfaces
{
    public interface IGenericRepository<T> where T : ModelBase
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetAsync(int id);
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);


    }
}
