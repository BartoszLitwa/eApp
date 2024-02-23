using System.ComponentModel.DataAnnotations;

namespace eApp.PlatformService.Api.Dtos.Platform;

public record PlatformCreateDto
{
    [Required] public string Name { get; set; }

    [Required] public string Publisher { get; set; }

    [Required] public string Cost { get; set; }
}