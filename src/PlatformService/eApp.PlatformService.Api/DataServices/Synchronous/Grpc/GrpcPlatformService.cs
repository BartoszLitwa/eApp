using AutoMapper;
using eApp.PlatformService.Api.Data;
using Grpc.Core;
using Microsoft.EntityFrameworkCore;

namespace eApp.PlatformService.Api.DataServices.Synchronous.Grpc;

public class GrpcPlatformService(AppDbContext _context, IMapper _mapper) 
    : PlatformService.GrpcPlatformService.GrpcPlatformServiceBase
{
    public override async Task<GetAllPlatformsResponse>  GetAllPlatforms(GetAllPlatformsRequest request, ServerCallContext context)
    {
        var platforms = await _context.Platforms.ToListAsync();
        var response = new GetAllPlatformsResponse();
        response.Platforms.AddRange(platforms.Select(_mapper.Map<GrpcPlatformModel>));
        return response;
    }
}