using AutoMapper;
using CSharpFunctionalExtensions;
using eApp.PlatformService.Api.Data;
using eApp.PlatformService.Domain.Dtos;
using eApp.PlatformService.Domain.Models;
using MediatR;

namespace eApp.PlatformService.Api.Platforms.Commands;

public record CreatePlatformCommand(string Name, string Publisher, string Cost)
    : IRequest<Result<PlatformReadDto, ValidationFailed>>;

public class CreatePlatformHandler(AppDbContext dbContext, IMapper mapper)
    : IRequestHandler<CreatePlatformCommand, Result<PlatformReadDto, ValidationFailed>>
{
    public async Task<Result<PlatformReadDto, ValidationFailed>> Handle(CreatePlatformCommand request, CancellationToken cancellationToken)
    {
        var created = await dbContext.Platforms
            .AddAsync(new Platform
            {
                Name = request.Name,
                Publisher = request.Publisher,
                Cost = request.Cost
            }, cancellationToken);
        
        await dbContext.SaveChangesAsync(cancellationToken);

        var platformReadDto = mapper.Map<PlatformReadDto>(created.Entity);

        return Result.Success<PlatformReadDto, ValidationFailed>(platformReadDto);
    }
}