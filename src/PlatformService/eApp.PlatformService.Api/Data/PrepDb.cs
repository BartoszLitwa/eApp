using eApp.PlatformService.Domain.Models;

namespace eApp.PlatformService.Api.Data;

public static class PrepDb
{
    public static void PrepPopulation(this WebApplication app)
    {
        using var serviceScope = app.Services.CreateScope();
        SeedData(serviceScope.ServiceProvider.GetService<AppDbContext>()!);
    }

    private static void SeedData(AppDbContext context)
    {
        if (context.Platforms.Any())
            return;

        Console.WriteLine("--> Seeding data...");
        context.Platforms.AddRange(
            new Platform { Name = "Dot Net", Publisher = "Microsoft", Cost = "Free" },
            new Platform { Name = "SQL Server Express", Publisher = "Microsoft", Cost = "Free" },
            new Platform { Name = "Kubernetes", Publisher = "Cloud Native Computing Foundation", Cost = "Free" }
        );

        context.SaveChanges();
    }
}