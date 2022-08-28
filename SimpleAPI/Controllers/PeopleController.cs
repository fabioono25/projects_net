using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
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
        [ActionName(nameof(GetPersonById))]
        public ActionResult<Person> GetPersonById(int id)
        {
            var person = _repository.GetPersonById(id);

            if (person == null)
                return NotFound();

            return Ok(_mapper.Map<PersonReadDto>(person));
        }

        [HttpPost]
        public ActionResult<PersonReadDto> CreatePerson(PersonCreateDto personDto)
        {
            var model = _mapper.Map<Person>(personDto);
            _repository.CreatePerson(model);
            _repository.SaveChanges();

            var personReadDto = _mapper.Map<PersonReadDto>(model);

            return CreatedAtRoute(nameof(GetPersonById), new { id = personReadDto.Id, personReadDto }); // it is not working at this moment
            // return Ok(personReadDto);
        }

        [HttpPut("{id}")]
        public ActionResult<PersonReadDto> UpdatePerson(int id, PersonUpdateDto personDto)
        {
            var model = _repository.GetPersonById(id);

            if (model == null)
                return NotFound();

            _mapper.Map(personDto, model);

            _repository.UpdatePerson(model); // not necessary
            _repository.SaveChanges();

            return NoContent();
        }

        [HttpPatch("{id}")]
        public ActionResult PartialUpdatePerson(int id, JsonPatchDocument<PersonUpdateDto> personPatch)
        {
            var model = _repository.GetPersonById(id);

            if (model == null)
                return NotFound();

            var personToPatch = _mapper.Map<PersonUpdateDto>(model);
            personPatch.ApplyTo(personToPatch, ModelState);

            if (!TryValidateModel(personToPatch))
                return ValidationProblem(ModelState);

            _mapper.Map(personToPatch, model);
            _repository.UpdatePerson(model); // not necessary
            _repository.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult<Person> DeletePerson(int id)
        {
            var model = _repository.GetPersonById(id);

            if (model == null)
                return NotFound();

            _repository.DeletePerson(model);
            _repository.SaveChanges();

            return NoContent();
        }
    }
}

