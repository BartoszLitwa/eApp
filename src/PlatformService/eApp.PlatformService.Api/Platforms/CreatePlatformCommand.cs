using eApp.PlatformService.Domain.Dtos;
using eApp.PlatformService.Domain.Models;
using MediatR;

namespace eApp.PlatformService.Api.Platforms;

public record CreatePlatformCommand() : IRequest<Result<PlatformReadDto, ValidationFailed>>;

public class CreateMovieHandler : IRequestHandler<CreatePlatformCommand, Result<PlatformReadDto, ValidationFailed>>;