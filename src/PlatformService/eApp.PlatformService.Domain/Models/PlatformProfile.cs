using AutoMapper;
using eApp.PlatformService.Domain.Dtos;

namespace eApp.PlatformService.Domain.Models;

public class PlatformProfile : Profile
{
    public PlatformProfile()
    {
        CreateMap<Platform, PlatformReadDto>();
        CreateMap<PlatformCreateDto, Platform>();
    }    
}