using Demo.Services.Abstraction;
using Demo.Services.Implementation;
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
            services.AddScoped<IAddressService, AddressService>();
            services.AddScoped<IPersonService, PersonService>();

            return services;
        }
    }
}
