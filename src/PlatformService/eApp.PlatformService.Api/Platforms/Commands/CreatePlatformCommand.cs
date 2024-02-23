using AutoMapper;
using CSharpFunctionalExtensions;
using eApp.CommandService.Api.EventProcessing;
using eApp.PlatformService.Api.Data;
using eApp.PlatformService.Api.DataServices.Asynchronous;
using eApp.PlatformService.Api.Dtos;
using eApp.PlatformService.Api.Dtos.Platform;
using eApp.PlatformService.Api.SyncDataServices.Http;
using eApp.PlatformService.Domain.Models;
using MediatR;

namespace eApp.PlatformService.Api.Platforms.Commands;

public record CreatePlatformCommand(string Name, string Publisher, string Cost)
    : IRequest<Result<PlatformReadDto, ValidationFailed>>;

public class CreatePlatformHandler(AppDbContext dbContext, IMapper mapper, ICommandDataClient commandDataClient, IMessageBusClient messageBusClient)
    : IRequestHandler<CreatePlatformCommand, Result<PlatformReadDto, ValidationFailed>>
{
    public async Task<Result<PlatformReadDto, ValidationFailed>> Handle(CreatePlatformCommand request, CancellationToken cancellationToken)
    {
        var created = await dbContext.Platforms
            .AddAsync(new Platform
            {
                Name = request.Name,
                Publisher = request.Publisher,
                Cost = request.Cost
            }, cancellationToken);
        
        await dbContext.SaveChangesAsync(cancellationToken);

        var platformReadDto = mapper.Map<PlatformReadDto>(created.Entity);

        // Send Synchronous Message
        try
        {
            await commandDataClient.SendPlatformToCommand(platformReadDto);
        }
        catch (Exception e)
        {
            Console.WriteLine($"--> Could not send synchronously: {e.Message}");
            throw;
        }
        
        // Send Asynchronous Message
        try
        {
            var published = mapper.Map<PlatformPublishedDto>(platformReadDto) 
                with { Event = PlatformEventType.PlatformPublished.ToString() };
            await messageBusClient.PublishNewPlatform(published, cancellationToken);
        }
        catch (Exception e)
        {
            Console.WriteLine($"--> Could not send asynchronously: {e.Message}");
            throw;
        }

        return Result.Success<PlatformReadDto, ValidationFailed>(platformReadDto);
    }
}