using System.Threading.Tasks;
using AutoMapper;
using Demo.CustomJsonConverter.Deserializer;
using Demo.Services.Abstraction;
using DomainModels.Dtos;
using DomainModels.Entities;
using Microsoft.AspNetCore.Mvc;
using Repository.RepositoryServices.Abstraction;

namespace Demo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonsController : ControllerBase
    {
        private readonly IPersonService _personService;

        private IUnitOfWork _unitOfWork { get; }
        private IMapper _mapper { get;}

        public PersonsController
            (IUnitOfWork unitOfWork
            ,IMapper mapper
            ,IPersonService personService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _personService = personService;
        }

        [HttpGet]
        public async Task<string> GetAllAsync([FromQuery] GetAllRequestDto request)
        {
            return await _personService.GetFilteredPeopleAsync(request, _unitOfWork, _mapper);
        }

        [HttpPost]
        public async Task<int> SaveAsync([FromBody] string json)
        {
            Person person= CustomDeserializer.Deserialize<Person>(json);
            return await _personService.AddPersonOrUpdateAsync(_unitOfWork, _mapper, person);
        }
    }
}
