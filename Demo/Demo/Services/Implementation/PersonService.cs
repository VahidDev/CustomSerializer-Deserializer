using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Demo.Services.Abstraction;
using Demo.Utilities.JsonUtilities;
using DomainModels.Dtos;
using DomainModels.Entities;
using Repository.RepositoryServices.Abstraction;

namespace Demo.Services.Implementation
{
    public class PersonService: IPersonService
    {
        private readonly IAddressService _addressService;

        public PersonService(IAddressService addressService)
        {
            _addressService = addressService;
        }

        public async Task<string> GetFilteredPeopleAsync
            (GetAllRequestDto dto, IUnitOfWork unitOfWork, IMapper mapper)
        {
            IList<Person> people = await unitOfWork.PersonRepository
                .FindAllAsync(p => 
                (string.IsNullOrEmpty(dto.City) ? true : p.Address.City.ToLower() == dto.City.ToLower()) &&
                (string.IsNullOrEmpty(dto.FirstName) ? true : p.FirstName.ToLower() == dto.FirstName.ToLower()) &&
                (string.IsNullOrEmpty(dto.LastName) ? true : p.LastName.ToLower() == dto.LastName.ToLower()),
                new List<string> { nameof(Address) });

            if (people == null || people.Count == 0)
            {
                return "No person found";
            }

            return CollectionSerlizerHelper.SerializeCollection<Person, PersonDto>(people, mapper);
        }

        public async Task<int> AddPersonOrUpdateAsync (IUnitOfWork unitOfWork, IMapper mapper, Person person)
        {
            bool isNull = await unitOfWork.PersonRepository.Exists(person.Id);

            if (isNull)
            {
                if (person.Address != null)
                {
                    await _addressService.AddAddressOrUpdateAsync
                        (unitOfWork, mapper, person);
                }

                person.Id = unitOfWork.PersonRepository.Update(person);
            }
            else
            {
                person.Id = 0;

                if (person.Address != null)
                {
                    person.Address = await _addressService.AddAddressOrUpdateAsync
                         (unitOfWork, mapper, person);
                }

                person.Id = await unitOfWork.PersonRepository.AddAsync(person);
            }

            await unitOfWork.CompleteAsync();
            return person.Id;
        }
    }
}
