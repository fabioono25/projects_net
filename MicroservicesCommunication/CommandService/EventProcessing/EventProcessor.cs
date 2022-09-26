using System;
using System.Text.Json;
using AutoMapper;
using CommandService.Data;
using CommandService.Dtos;
using CommandService.Models;
using Microsoft.Extensions.DependencyInjection;

namespace CommandService.EventProcessing
{
    public class EventProcessor : IEventProcessor
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly IMapper _mapper;

        public EventProcessor(IServiceScopeFactory serviceScopeFactory, IMapper mapper)
        {
            // this approach is because we cannot inject Repository as Scoped in Startup
            _serviceScopeFactory = serviceScopeFactory;
            _mapper = mapper;
        }

        public void ProcessEvent(string message)
        {
            var eventType = DetermineEventType(message);

            switch (eventType)
            {
                case EventType.PlatformPublished:
                    break;
                default:
                    break;
            }
        }

        private EventType DetermineEventType(string notificationMessage)
        {
            Console.WriteLine("--determine event");

            var eventType = JsonSerializer.Deserialize<GenericEventDto>(notificationMessage);

            switch (eventType.Event)
            {
                case "Platform_Published":
                    Console.WriteLine("-- Platform Published Event detected");
                    return EventType.PlatformPublished;
                default:
                    Console.WriteLine("-- EventType not determined");
                    return EventType.Undetermined;
            }
        }

        private void AddPlatform(string platformPublishMessage)
        {
            using var scope = _serviceScopeFactory.CreateScope();

            // solving LifeTime here
            var repo = scope.ServiceProvider.GetRequiredService<ICommandRepository>();

            var platformPublishedDto = JsonSerializer.Deserialize<PlatformPublishedDto>(platformPublishMessage);

            try
            {
                var platform = _mapper.Map<Platform>(platformPublishedDto);

                if (!repo.ExternalPlatformExists(platform.ExternalId))
                {
                    repo.CreatePlatform(platform);
                    repo.SaveChanges();
                } else
                    Console.WriteLine("-- Platform already exists...");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"-- Platform not added to DB: {ex.Message}.");
            }
        }
    }

    enum EventType
    {
        PlatformPublished,
        Undetermined
    }
}

