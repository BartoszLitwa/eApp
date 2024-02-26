using eApp.CommandService.Domain.Models;

namespace eApp.CommandService.Api.DataServices.Synchronous.Grpc;

public interface IPlatformDataClient
{
    Task<IEnumerable<Platform>> GetAllPlatforms();
}