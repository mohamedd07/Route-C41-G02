using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.Extensions.DependencyInjection;
using Route_C41_G02_BLL;
using Route_C41_G02_BLL.Interfaces;
using Route_C41_G02_BLL.Repositories;

namespace Route_C41_G02_PL.Extensions
{
    public static class AppServiceextensions
    {
        public static IServiceCollection AddAppService(this IServiceCollection service)
        {
            service.AddScoped<IUnitOfWork, UnitOfWork>();

            return service;
        }
    }
}
