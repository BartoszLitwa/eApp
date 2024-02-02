using AutoMapper;
using eApp.CommandService.Api.Dtos;
using eApp.CommandService.Domain.Models;

namespace eApp.CommandService.Api.Utils;

public class CommandProfile : Profile
{
    public CommandProfile()
    {
        CreateMap<Command, CommandReadDto>();
        CreateMap<CommandCreateDto, Command>();
        CreateMap<Platform, PlatformReadDto>();
    }
}