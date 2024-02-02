using AutoMapper;
using CSharpFunctionalExtensions;
using eApp.CommandService.Api.Data;
using eApp.CommandService.Api.Dtos;
using eApp.CommandService.Domain.Models;
using eApp.PlatformService.Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace eApp.CommandService.Api.Commands.Commands;

public record CreateCommandForPlatformIdCommand(int PlatformId, CommandCreateDto CommandCreateDto)
    : IRequest<Result<CommandReadDto, ValidationFailed>>;

public class CreateCommandForPlatformIdCommandHandler(AppDbContext _context, IMapper _mapper)
    : IRequestHandler<CreateCommandForPlatformIdCommand, Result<CommandReadDto, ValidationFailed>>
{
    public async Task<Result<CommandReadDto, ValidationFailed>> Handle(CreateCommandForPlatformIdCommand request, CancellationToken cancellationToken)
    {
        var exists = await _context.Platforms
            .AnyAsync(e => e.ExternalId == request.PlatformId, cancellationToken);
        if (!exists)
            return Result.Failure<CommandReadDto, ValidationFailed>(new ValidationFailed("Platform not found"));

        await using var transaction = await _context.Database.BeginTransactionAsync(cancellationToken);

        try
        {
            var command = _mapper.Map<Command>(request.CommandCreateDto);
            command.PlatformId = request.PlatformId;
    
            await _context.Commands.AddAsync(command, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        
            var mapped = _mapper.Map<CommandReadDto>(command);
    
            return Result.Success<CommandReadDto, ValidationFailed>(mapped);
        }
        catch (Exception e)
        {
            await transaction.RollbackAsync(cancellationToken);
            
            return Result.Failure<CommandReadDto, ValidationFailed>(new ValidationFailed(e.Message));
        }
    }
}