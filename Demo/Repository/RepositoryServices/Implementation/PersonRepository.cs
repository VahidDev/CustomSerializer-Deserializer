using DomainModels.Entities;
using Microsoft.Extensions.Logging;
using Repository.DAL;
using Repository.RepositoryServices.Abstraction;

namespace Repository.RepositoryServices.Implementation
{
    public class PersonRepository:GenericRepository<Person>,IPersonRepository
    {
        public PersonRepository(AppDbContext context, ILogger logger) : base(context, logger) { }
    }
}
