using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Demo.CustomJsonConverter.Deserializer;
using Demo.Services;
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
        private IUnitOfWork _unitOfWork { get; }
        private IMapper _mapper { get;}
        public PersonsController(IUnitOfWork unitOfWork,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<string> GetAll([FromQuery] GetAllRequestDto request)
        {
            return await PersonService
                .GetFilteredPeopleAsync(request, _unitOfWork, _mapper);
        }
        [HttpPost]
        public async Task<int> Save([FromBody] string json)
        {
            Person person= CustomDeserializer.Deserialize<Person>(json);
            return await PersonService
                .AddPersonOrUpdateAsync(_unitOfWork, _mapper, person);
        }
    }
}
