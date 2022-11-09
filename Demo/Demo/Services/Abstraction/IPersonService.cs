using AutoMapper;
using DomainModels.Dtos;
using DomainModels.Entities;
using Repository.RepositoryServices.Abstraction;
using System.Threading.Tasks;

namespace Demo.Services.Abstraction
{
    public interface IPersonService
    {
        Task<string> GetFilteredPeopleAsync(GetAllRequestDto dto, IUnitOfWork unitOfWork, IMapper mapper);
        Task<int> AddPersonOrUpdateAsync(IUnitOfWork unitOfWork, IMapper mapper, Person person);
    }
}
