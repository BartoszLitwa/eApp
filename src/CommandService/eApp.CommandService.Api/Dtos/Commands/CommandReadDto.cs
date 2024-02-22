namespace eApp.CommandService.Api.Dtos.Commands;

public record CommandReadDto(int Id, string HowTo, string CommandLine, int PlatformId);