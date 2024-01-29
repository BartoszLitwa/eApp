using AutoMapper;
using CSharpFunctionalExtensions;
using eApp.PlatformService.Api.Data;
using eApp.PlatformService.Domain.Dtos;
using eApp.PlatformService.Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace eApp.PlatformService.Api.Platforms.Queries;

public record GetPlatformByIdQuery(int Id) : IRequest<Result<PlatformReadDto, ValidationFailed>>;

public class GetPlatformByIdHandler(AppDbContext dbContext, IMapper mapper)
    : IRequestHandler<GetPlatformByIdQuery, Result<PlatformReadDto, ValidationFailed>>
{
    public async Task<Result<PlatformReadDto, ValidationFailed>> Handle(GetPlatformByIdQuery request, CancellationToken cancellationToken)
    {
        var platform = await dbContext.Platforms
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        
        if(platform is null)
            return Result.Failure<PlatformReadDto, ValidationFailed>(new ValidationFailed($"Platform with Id: {request.Id} not found"));

        var platformDto = mapper.Map<PlatformReadDto>(platform);

        return Result.Success<PlatformReadDto, ValidationFailed>(platformDto);
    }
}