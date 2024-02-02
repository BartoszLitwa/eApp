using eApp.PlatformService.Api.Dtos.Platform;

namespace eApp.PlatformService.Api.DataServices.Asynchronous;

public interface IMessageBusClient
{
    ValueTask PublishNewPlatform(PlatformPublishedDto platformPublishedDto, CancellationToken cancellationToken);
}