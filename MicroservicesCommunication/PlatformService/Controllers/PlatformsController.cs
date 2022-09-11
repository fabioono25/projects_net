using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PlatformService.Data;
using PlatformService.Dtos;
using PlatformService.Models;
using PlatformService.SyncDataServices.Http;

namespace PlatformService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlatformsController : ControllerBase
    {
        private readonly IPlatformRepo _repository;
        private readonly IMapper _mapper;
        private readonly ICommandDataClient _command;

        public PlatformsController(IPlatformRepo repository, IMapper mapper, ICommandDataClient command)
        {
            _repository = repository;
            _mapper = mapper;
            _command = command;
        }

        [HttpGet]
        public ActionResult<IEnumerable<PlatformReadDto>> GetAllPlatforms()
        {
            Console.WriteLine("Getting Pplatforms...");

            var platforms = _repository.GetAllPlatforms();

            return Ok(_mapper.Map<IEnumerable<PlatformReadDto>>(platforms));
        }

        [HttpGet("{id}", Name = "GetPlatformsById")]
        public ActionResult<PlatformReadDto> GetPlatformsById(int id)
        {
            Console.WriteLine($"Getting Platform by Id {id}...");

            var platform = _repository.GetPlatformById(id);

            if (platform == null)
                return NotFound();

            return Ok(_mapper.Map<PlatformReadDto>(platform));
        }

        [HttpPost]
        public async Task<ActionResult<PlatformReadDto>> CreatePlatform(PlatformCreateDto platformCreateDto)
        {
            Console.WriteLine($"Creating a Pplatform: {platformCreateDto.Name}...");

            var platform = _mapper.Map<Platform>(platformCreateDto);

            _repository.CreatePlatform(platform);
            _repository.SaveChanges();

            var platformReadDto = _mapper.Map<PlatformReadDto>(platform);

            try
            {
                await _command.SendPlatformToCommand(platformReadDto);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"-- Could not send synchronously (from Platform to Command service): {ex.Message}.");
            }

            return CreatedAtRoute(nameof(GetPlatformsById) , new { Id = platformReadDto.Id }, platformReadDto);
        }
    }
}
