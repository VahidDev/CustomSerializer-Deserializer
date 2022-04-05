using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Demo.Utilities.JsonUtilities;
using DomainModels.Dtos;
using DomainModels.Entities;
using Repository.RepositoryServices.Abstraction;

namespace Demo.Services
{
    public class PersonService
    {
        public async static Task<string> GetFilteredPeopleAsync
            (GetAllRequestDto dto, IUnitOfWork unitOfWork,IMapper mapper)
        {
            //Find filtered people and include their adresses
            IList<Person> people = await unitOfWork.PersonRepository
                .FindAllAsync(p => (
                (string.IsNullOrEmpty(dto.City) ? true : 
                (p.Address.City.ToLower() == dto.City.ToLower())) &&
                (string.IsNullOrEmpty(dto.FirstName) ? true : 
                (p.FirstName.ToLower() == dto.FirstName.ToLower())) &&
                (string.IsNullOrEmpty(dto.LastName) ? true : 
                (p.LastName.ToLower() == dto.LastName.ToLower()))
                ),new List<string> { nameof(Address) });
            if (people == null||people.Count==0) return "No person found";
            return CollectionSerlizerHelper.SerializeCollection<Person,PersonDto>(people,mapper);
        }
        public static async Task<int> AddPersonOrUpdateAsync
          (IUnitOfWork unitOfWork, IMapper mapper, Person person)
        {
            bool isNull = await unitOfWork
                .PersonRepository.Exists(person.Id);
            if (isNull)
            {
                if (person.Address != null)
                {
                    await AddressService.AddAddressOrUpdateAsync
                        (unitOfWork, mapper, person);
                }
                person.Id=unitOfWork.PersonRepository.Update(person);
            }
            else
            {
                //To avoid conflict when adding db we set it to 0
                person.Id = 0;
                if (person.Address != null)
                {
                    person.Address=await AddressService.AddAddressOrUpdateAsync
                         (unitOfWork, mapper, person);
                }
                person.Id = await unitOfWork.PersonRepository.AddAsync(person);
            }
            await unitOfWork.CompleteAsync();
            return person.Id;
        }
    }
}
