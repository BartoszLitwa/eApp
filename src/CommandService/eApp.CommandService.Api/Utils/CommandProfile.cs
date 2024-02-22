using AutoMapper;
using eApp.CommandService.Api.Dtos;
using eApp.CommandService.Api.Dtos.Commands;
using eApp.CommandService.Api.Dtos.Platforms;
using eApp.CommandService.Domain.Models;

namespace eApp.CommandService.Api.Utils;

public class CommandProfile : Profile
{
    public CommandProfile()
    {
        CreateMap<Command, CommandReadDto>();
        CreateMap<CommandCreateDto, Command>();
        CreateMap<Platform, PlatformReadDto>();

        CreateMap<PlatformPublishedDto, Platform>()
            .ForMember(dest => dest.ExternalId, opt => opt.MapFrom(src => src.Id));
    }
}