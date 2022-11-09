using System.Threading.Tasks;
using AutoMapper;
using Demo.Services.Abstraction;
using DomainModels.Entities;
using Repository.RepositoryServices.Abstraction;

namespace Demo.Services.Implementation
{
    public class AddressService : IAddressService
    {
        public async Task<Address> AddAddressOrUpdateAsync
            (IUnitOfWork unitOfWork, IMapper mapper, Person person)
        {
            Address address = await unitOfWork.AddressRepository
                .FirstOrDefaultAsync(a => a.City == person.Address.City &&
                                  a.AddressLine == person.Address.AddressLine);

            if (address == null)
            {
                address = person.Address;
                person.AddressId = await unitOfWork.AddressRepository
                      .AddAsync(mapper.Map<Address>(person.Address));
            }
            else
            {
                person.Address = address;
            }

            return address;
        }
    }
}
