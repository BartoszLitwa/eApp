namespace eApp.CommandService.Api.Dtos;

public record CommandReadDto(int Id, string HowTo, string CommandLine, int PlatformId);