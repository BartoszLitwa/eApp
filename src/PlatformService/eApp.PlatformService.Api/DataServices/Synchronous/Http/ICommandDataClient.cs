using eApp.PlatformService.Api.Dtos;
using eApp.PlatformService.Api.Dtos.Platform;

namespace eApp.PlatformService.Api.SyncDataServices.Http;

public interface ICommandDataClient
{
    Task SendPlatformToCommand(PlatformReadDto platform);
}