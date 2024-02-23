using System.ComponentModel.DataAnnotations;

namespace eApp.PlatformService.Api.Dtos.Platform;

public record PlatformReadDto
{
    [Required] public int Id { get; init; }
    [Required] public string Name { get; init; }

    [Required] public string Publisher { get; init; }

    [Required] public string Cost { get; init; }
}
