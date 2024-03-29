﻿using AutoMapper;
using eApp.PlatformService.Api.Dtos;
using eApp.PlatformService.Api.Dtos.Platform;
using eApp.PlatformService.Domain.Models;

namespace eApp.PlatformService.Api.Utils;

public class PlatformProfile : Profile
{
    public PlatformProfile()
    {
        CreateMap<Platform, PlatformReadDto>();
        CreateMap<PlatformCreateDto, Platform>();
        CreateMap<PlatformReadDto, PlatformPublishedDto>();
        CreateMap<Platform, GrpcPlatformModel>()
            .ForMember(dest => dest.PlatformId, opt => opt.MapFrom(src => src.Id));
    }    
}