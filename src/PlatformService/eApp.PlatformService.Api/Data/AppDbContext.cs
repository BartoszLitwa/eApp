using eApp.PlatformService.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace eApp.PlatformService.Api.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public virtual DbSet<Platform> Platforms { get; init; }
}