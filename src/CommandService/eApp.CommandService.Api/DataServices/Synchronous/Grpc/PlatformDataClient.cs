using AutoMapper;
using eApp.CommandService.Domain.Models;
using Grpc.Net.Client;
using Microsoft.Extensions.Options;

namespace eApp.CommandService.Api.DataServices.Synchronous.Grpc;

public class PlatformDataClient(
    IOptions<GrpcConfg> _grpcOptions,
    IOptions<AppConfig> _appConfigOptions,
    IMapper _mapper)
    : IPlatformDataClient
{
    public async Task<IEnumerable<Platform>> GetAllPlatforms()
    {
        Console.WriteLine($"--> Calling GRPC Service: {_grpcOptions.Value.GetPlatformProtoPath}");
        var channel = GrpcChannel.ForAddress(_appConfigOptions.Value.PlatformServiceGrpcUrl);
        var client = new GrpcPlatformService.GrpcPlatformServiceClient(channel);
        var request = new GetAllPlatformsRequest();

        try
        {
            var reply = await client.GetAllPlatformsAsync(request);
            return _mapper.Map<IEnumerable<Platform>>(reply.Platforms);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}