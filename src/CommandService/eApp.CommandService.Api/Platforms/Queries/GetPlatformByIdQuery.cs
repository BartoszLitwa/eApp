using AutoMapper;
using CSharpFunctionalExtensions;
using eApp.CommandService.Api.Data;
using eApp.CommandService.Api.Dtos;
using eApp.CommandService.Api.Dtos.Platforms;
using eApp.PlatformService.Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace eApp.CommandService.Api.Platforms.Queries;

public record GetPlatformByIdQuery(int Id) : IRequest<Result<PlatformReadDto, ValidationFailed>>;

public class GetPlatformByIdQueryHandler(AppDbContext _context, IMapper _mapper) 
    : IRequestHandler<GetPlatformByIdQuery, Result<PlatformReadDto, ValidationFailed>>
{
    public async Task<Result<PlatformReadDto, ValidationFailed>> Handle(GetPlatformByIdQuery request, CancellationToken cancellationToken)
    {
        var platform = await _context.Platforms
            .FirstOrDefaultAsync(e => e.Id == request.Id, cancellationToken);

        if (platform is null)
            return Result.Failure<PlatformReadDto, ValidationFailed>(
                new ValidationFailed($"Platform with Id: {request.Id} not found"));
        
        var mapped =  _mapper.Map<PlatformReadDto>(platform);

        return Result.Success<PlatformReadDto, ValidationFailed>(mapped);
    }
}
