using eApp.PlatformService.Api.Dtos;

namespace eApp.PlatformService.Api.SyncDataServices.Http;

public interface ICommandDataClient
{
    Task SendPlatformToCommand(PlatformReadDto platform);
}