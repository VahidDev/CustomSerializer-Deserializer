using System.Threading.Tasks;
using AutoMapper;
using DomainModels.Entities;
using Repository.RepositoryServices.Abstraction;

namespace Demo.Services
{
    public class AddressService
    {
        public static async Task<Address> AddAddressOrUpdateAsync
            (IUnitOfWork unitOfWork,IMapper mapper,Person person)
        {
            //City and AddressLine together are unique so we find by city name and addressline
            //instead of id
            Address address = await unitOfWork.AddressRepository
                  .FirstOrDefaultAsync
                  (a => 
                  a.City == person.Address.City&& a.AddressLine == person.Address.AddressLine
                  );
            // if there is no such address then add it to db
            if (address == null)
            {
                address = person.Address;
                person.AddressId = await unitOfWork.AddressRepository
                      .AddAsync(mapper.Map<Address>(person.Address));
            }
            else
            {
                person.Address=address;
            }
            return address;
        }
    }
}
