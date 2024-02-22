using System.Text.Json;
using AutoMapper;
using eApp.CommandService.Api.Data;
using eApp.CommandService.Api.Dtos.Platforms;
using eApp.CommandService.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace eApp.CommandService.Api.EventProcessing.EventTypeProcessors;

public class PlatformPublishedEventProcessor : EventProcessorBase
{
    public override PlatformEventType EventType => PlatformEventType.PlatformPublished;

    public override async ValueTask<bool> ProcessEvent(IServiceProvider serviceProvider, IMapper mapper, string message, CancellationToken cancellationToken)
    {
        var platformPublished = JsonSerializer.Deserialize<PlatformPublishedDto>(message);
        var platform = mapper.Map<Platform>(platformPublished);
        
        var context = serviceProvider.GetRequiredService<AppDbContext>();

        await using var transaction = await context.Database.BeginTransactionAsync(cancellationToken);
        try
        {
            var platformExists = await context.Platforms.AnyAsync(e => e.ExternalId == platform.ExternalId, cancellationToken: cancellationToken);
            if (platformExists)
            {
                Console.WriteLine("--> Platform already exists");
                return true;
            }
            
            await context.Platforms.AddAsync(platform, cancellationToken);
            await context.SaveChangesAsync(cancellationToken);

            await transaction.CommitAsync(cancellationToken);
            
            return true;
        }
        catch (Exception e)
        {
            await transaction.RollbackAsync(cancellationToken);
            
            Console.WriteLine($"--> Could not add Platform to DB: {e}");
            return false;
        }
    }
}