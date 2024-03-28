using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.Extensions.DependencyInjection;
using Route_C41_G02_BLL.Interfaces;
using Route_C41_G02_BLL.Repositories;

namespace Route_C41_G02_PL.Extensions
{
    public static class AppServiceextensions
    {
        public static void AddAppService(this IServiceCollection service)
        {
            //services.AddTransient<IDepartmentRepository, DepartmentRepository>();
            //services.AddSingleton<IDepartmentRepository, DepartmentRepository>();
            service.AddScoped<IDepartmentRepository, DepartmentRepository>();

            service.AddScoped<IEmployeeRepository, EmployeeRepository>();
        }
    }
}
