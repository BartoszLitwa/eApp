using AutoMapper;
using CSharpFunctionalExtensions;
using eApp.CommandService.Api.Data;
using eApp.CommandService.Api.Dtos;
using eApp.CommandService.Api.Dtos.Commands;
using eApp.PlatformService.Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace eApp.CommandService.Api.Commands.Queries;

public record GetCommandByIdForPlatformIdQuery(int PlatformId, int CommandId)
    : IRequest<Result<CommandReadDto, ValidationFailed>>;

public class GetCommandByIdForPlatformIdQueryHandler(AppDbContext _context, IMapper _mapper) 
    : IRequestHandler<GetCommandByIdForPlatformIdQuery, Result<CommandReadDto, ValidationFailed>>
{
    public async Task<Result<CommandReadDto, ValidationFailed>> Handle(GetCommandByIdForPlatformIdQuery request, CancellationToken cancellationToken)
    {
        var exists = await _context.Platforms
            .AnyAsync(e => e.ExternalId == request.PlatformId, cancellationToken);
        if (!exists)
            return Result.Failure<CommandReadDto, ValidationFailed>(new ValidationFailed("Platform not found"));
        
        var command = await _context.Commands
            .FirstOrDefaultAsync(e => e.PlatformId == request.PlatformId && e.Id == request.CommandId, cancellationToken);
        if (command is null)
            return Result.Failure<CommandReadDto, ValidationFailed>(new ValidationFailed("Command not found"));
        
        var mapped = _mapper.Map<CommandReadDto>(command);
        
        return Result.Success<CommandReadDto, ValidationFailed>(mapped);
    }
}