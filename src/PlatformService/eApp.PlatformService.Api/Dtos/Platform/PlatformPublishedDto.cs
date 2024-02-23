namespace eApp.PlatformService.Api.Dtos.Platform;

public record PlatformPublishedDto
{
    public int Id { get; init; }
    public string Name { get; init; }
    public string Event { get; init; }
}