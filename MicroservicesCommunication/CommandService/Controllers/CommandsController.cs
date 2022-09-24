using System;
using AutoMapper;
using CommandService.Data;
using CommandService.Dtos;
using CommandService.Models;
using Microsoft.AspNetCore.Mvc;

namespace CommandService.Controllers
{
    [ApiController]
    [Route("api/c/platforms/{platformId}/[controller]")]
    public class CommandsController : ControllerBase
    {
        private readonly ICommandRepository _repository;
        private readonly IMapper _mapper;

        public CommandsController(ICommandRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<CommandReadDto>> GetCommandsForPlatform(int platformId)
        {
            Console.WriteLine("--> getting Commands for Platform");

            if (!_repository.PlatformExists(platformId))
                return NotFound();

            var commands = _repository.GetCommandsForPlatform(platformId);

            return Ok(_mapper.Map<IEnumerable<CommandReadDto>>(commands));
        }

        [HttpGet("{commandId}", Name = "GetCommandForPlatform")]
        public ActionResult<CommandReadDto> GetCommandForPlatform(int platformId, int commandId)
        {
            Console.WriteLine($"--> getting Commands for Platform: {platformId} - {commandId}");

            if (!_repository.PlatformExists(platformId))
                return NotFound();

            var command = _repository.GetCommand(platformId, commandId);

            if (command == null)
                return NotFound();

            return Ok(_mapper.Map<CommandReadDto>(command));
        }

        [HttpPost]
        public ActionResult<CommandReadDto> CreateCommandForPlatform(int platformId, CommandCreateDto commandDto)
        {
            Console.WriteLine($"Creating Command for Platform: {platformId}.");

            if (!_repository.PlatformExists(platformId))
                return NotFound();

            //_repository.CreateCommand(platformId, command);
            //_repository.SaveChanges();

            //var commandReadDto = _mapper.Map<CommandReadDto>(command);

            try
            {
                var command = _mapper.Map<Command>(commandDto);

                _repository.CreateCommand(platformId, command);
                _repository.SaveChanges();

                var commandReadDto = _mapper.Map<CommandReadDto>(command);

                return CreatedAtRoute(nameof(GetCommandForPlatform),
                    new { PlatformId = platformId, CommandId = commandReadDto.Id }, commandReadDto);

            }
            catch (Exception ex)
            {
                Console.WriteLine($"-- Could not create a command for the platform: {ex.Message}.");
                return BadRequest();
            }
        }
    }
}

