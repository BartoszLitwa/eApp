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
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.ExternalId, opt => opt.MapFrom(src => src.Id));

        CreateMap<GrpcPlatformModel, Platform>()
            .ForMember(dest => dest.Commands, opt => opt.Ignore())
            .ForMember(dest => dest.ExternalId, opt => opt.MapFrom(src => src.PlatformId));
    }
}