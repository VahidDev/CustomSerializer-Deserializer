using AutoMapper;
using DomainModels.Entities;
using Repository.RepositoryServices.Abstraction;
using System.Threading.Tasks;

namespace Demo.Services.Abstraction
{
    public interface IAddressService
    {
        Task<Address> AddAddressOrUpdateAsync(IUnitOfWork unitOfWork, IMapper mapper, Person person);
    }
}
