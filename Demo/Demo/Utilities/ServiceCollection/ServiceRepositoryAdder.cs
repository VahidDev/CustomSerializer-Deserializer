using Microsoft.Extensions.DependencyInjection;
using Repository.RepositoryServices.Abstraction;
using Repository.RepositoryServices.Implementation;

namespace Demo.Utilities.ServiceCollection
{
    public static class ServiceRepositoryAdder
    {
        public static IServiceCollection AddRepository(this IServiceCollection services)
        {
            services.AddScoped(typeof(IGenericRepository<>),typeof(GenericRepository<>));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            return services;
        }
    }
}
