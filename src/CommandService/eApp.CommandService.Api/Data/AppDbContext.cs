using eApp.CommandService.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace eApp.CommandService.Api.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public virtual DbSet<Platform> Platforms { get; init; }
    public virtual DbSet<Command> Commands { get; init; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Platform>(entity =>
        {
            entity.HasMany(p => p.Commands)
                .WithOne(p => p.Platform)
                .HasForeignKey(p => p.PlatformId);
        });

        modelBuilder.Entity<Command>(entity =>
        {
            entity.HasOne(p => p.Platform)
                .WithMany(p => p.Commands)
                .HasForeignKey(p => p.PlatformId);
        });
    }
}