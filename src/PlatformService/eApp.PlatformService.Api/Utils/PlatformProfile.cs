using AutoMapper;
using eApp.PlatformService.Api.Dtos;
using eApp.PlatformService.Domain.Dtos;
using eApp.PlatformService.Domain.Models;

namespace eApp.PlatformService.Api.Utils;

public class PlatformProfile : Profile
{
    public PlatformProfile()
    {
        CreateMap<Platform, PlatformReadDto>();
        CreateMap<PlatformCreateDto, Platform>();
    }    
}