using AutoMapper;
using CSharpFunctionalExtensions;
using eApp.CommandService.Api.Data;
using eApp.CommandService.Api.Dtos;
using eApp.PlatformService.Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace eApp.CommandService.Api.Commands.Queries;

public record GetCommandsForPlatformIdQuery(int PlatformId)
    : IRequest<Result<IEnumerable<CommandReadDto>, ValidationFailed>>;

public class GetCommandsForPlatformIdQueryHandler(AppDbContext _context, IMapper _mapper)
    : IRequestHandler<GetCommandsForPlatformIdQuery, Result<IEnumerable<CommandReadDto>, ValidationFailed>>
{
    public async Task<Result<IEnumerable<CommandReadDto>, ValidationFailed>> Handle(GetCommandsForPlatformIdQuery request, CancellationToken cancellationToken)
    {
        var exists = await _context.Platforms
            .AnyAsync(e => e.ExternalId == request.PlatformId, cancellationToken);
        
        if (!exists)
            return Result.Failure<IEnumerable<CommandReadDto>, ValidationFailed>(new ValidationFailed("Platform not found"));
        
        var commands = await _context.Commands
            .Where(e => e.PlatformId == request.PlatformId)
            .ToListAsync(cancellationToken);

        var mapped = _mapper.Map<IEnumerable<CommandReadDto>>(commands);
        
        return Result.Success<IEnumerable<CommandReadDto>, ValidationFailed>(mapped);
    }
}