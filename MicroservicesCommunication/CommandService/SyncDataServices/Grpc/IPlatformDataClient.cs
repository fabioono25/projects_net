using System;
using CommandService.Models;

namespace CommandService.SyncDataServices.Grpc
{
	public interface IPlatformDataClient
	{
        IEnumerable<Platform> ReturnAllPlatforms();
    }
}

