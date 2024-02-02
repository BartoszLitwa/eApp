using AutoMapper;
using CSharpFunctionalExtensions;
using eApp.CommandService.Api.Data;
using eApp.CommandService.Api.Dtos;
using eApp.PlatformService.Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace eApp.CommandService.Api.Platforms.Queries;

public record GetPlatformsQuery() : IRequest<Result<IEnumerable<PlatformReadDto>, ValidationFailed>>;

public class GetPlatformsQueryHandler(AppDbContext _context, IMapper _mapper) 
    : IRequestHandler<GetPlatformsQuery, Result<IEnumerable<PlatformReadDto>, ValidationFailed>>
{
    public async Task<Result<IEnumerable<PlatformReadDto>, ValidationFailed>> Handle(GetPlatformsQuery request, CancellationToken cancellationToken)
    {
        var platforms = await _context.Platforms
            .ToListAsync(cancellationToken);
        
        var mapped =  _mapper.Map<IEnumerable<PlatformReadDto>>(platforms);

        return Result.Success<IEnumerable<PlatformReadDto>, ValidationFailed>(mapped);
    }
}
