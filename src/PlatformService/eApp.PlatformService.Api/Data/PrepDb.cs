using eApp.PlatformService.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace eApp.PlatformService.Api.Data;

public static class PrepDb
{
    public static void PrepPopulation(this WebApplication app, bool isProduction)
    {
        using var serviceScope = app.Services.CreateScope();
        SeedData(serviceScope.ServiceProvider.GetService<AppDbContext>(), isProduction);
    }

    private static void SeedData(AppDbContext context, bool isProduction)
    {
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