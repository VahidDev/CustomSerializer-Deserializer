using DomainModels.Entities;
using Microsoft.Extensions.Logging;
using Repository.DAL;
using Repository.RepositoryServices.Abstraction;

namespace Repository.RepositoryServices.Implementation
{
    public class AddressRepository:GenericRepository<Address>,IAddressRepository
    {
        public AddressRepository(AppDbContext context,ILogger logger) : base(context, logger) { }
    }
}
