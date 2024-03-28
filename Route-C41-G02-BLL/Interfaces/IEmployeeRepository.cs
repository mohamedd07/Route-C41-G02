using Route_C41_G02_DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Route_C41_G02_BLL.Interfaces
{
    public interface IEmployeeRepository : GenericRepository<Employee>
    {
        IQueryable<Employee> SearchByName(string name);
        IQueryable<Employee> GetEmployeeByAddress(string address);
    }


}
