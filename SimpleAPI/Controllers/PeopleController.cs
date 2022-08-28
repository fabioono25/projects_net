using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SimpleAPI.Data;
using SimpleAPI.Dtos;
using SimpleAPI.Models;

namespace SimpleAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PeopleController : ControllerBase
    {
        private readonly IPersonRepo _repository;
        private readonly IMapper _mapper;

        public PeopleController(IPersonRepo repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<PersonReadDto>> GetAllPeople()
        {
            var people = _repository.GetAllPeople();

            return Ok(_mapper.Map<IEnumerable<PersonReadDto>>(people));
        }

        [HttpGet("{id}")]
        public ActionResult<Person> GetPersonById(int id)
        {
            var person = _repository.GetPersonById(id);

            if (person == null)
                return NotFound();

            return Ok(_mapper.Map<PersonReadDto>(person));
        }
    }
}

