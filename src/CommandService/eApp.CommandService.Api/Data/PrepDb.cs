using eApp.CommandService.Api.DataServices.Synchronous.Grpc;
using eApp.CommandService.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace eApp.CommandService.Api.Data;

public static class PrepDb
{
    public static void PrepPopulation(this WebApplication app, bool isProduction)
    {
        using var serviceScope = app.Services.CreateScope();
        SeedData(serviceScope.ServiceProvider, isProduction);
    }

    private static void SeedData(IServiceProvider serviceProvider, bool isProduction)
    {
        var context = serviceProvider.GetService<AppDbContext>();
        context.Database.EnsureCreated();
        if (isProduction)
        {
            Console.WriteLine("--> Attempting to apply migrations...");
            try
            {
                context.Database.Migrate();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"--> Could not run migrations: {ex.Message}");
            }
        }
        
        var platformsToPopulate = GetPlatformsToPopulate(serviceProvider);
        var platformPopulated = context.Platforms.ToList();
        
        Console.WriteLine("--> Seeding data...");
        context.Platforms.AddRange(platformsToPopulate
            .Where(p => platformPopulated.All(pp => pp.ExternalId != p.ExternalId)));

        context.SaveChanges();
    }

    private static IEnumerable<Platform> GetPlatformsToPopulate(IServiceProvider serviceProvider)
    {
        var platformClient = serviceProvider.GetRequiredService<IPlatformDataClient>();
        return platformClient.GetAllPlatforms()
            .ConfigureAwait(false)
            .GetAwaiter().GetResult();
    }
}