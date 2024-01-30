using AutoMapper;
using CSharpFunctionalExtensions;
using eApp.PlatformService.Api.Data;
using eApp.PlatformService.Api.Dtos;
using eApp.PlatformService.Domain.Dtos;
using eApp.PlatformService.Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace eApp.PlatformService.Api.Platforms.Queries;

public record GetAllPlatformsQuery() : IRequest<Result<IEnumerable<PlatformReadDto>, ValidationFailed>>;

public class GetAllPlatformsHandler(AppDbContext dbContext, IMapper mapper)
    : IRequestHandler<GetAllPlatformsQuery, Result<IEnumerable<PlatformReadDto>, ValidationFailed>>
{
    public async Task<Result<IEnumerable<PlatformReadDto>, ValidationFailed>> Handle(GetAllPlatformsQuery request, CancellationToken cancellationToken)
    {
        var platforms = await dbContext.Platforms
            .ToListAsync(cancellationToken: cancellationToken);

        var platformsDtos= mapper.Map<List<PlatformReadDto>>(platforms);

        return Result.Success<IEnumerable<PlatformReadDto>, ValidationFailed>(platformsDtos);
    }
}