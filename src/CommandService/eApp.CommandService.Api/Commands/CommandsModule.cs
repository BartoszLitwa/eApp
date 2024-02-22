using Carter;
using eApp.CommandService.Api.Commands.Commands;
using eApp.CommandService.Api.Commands.Queries;
using eApp.CommandService.Api.Dtos;
using eApp.CommandService.Api.Dtos.Commands;
using eApp.Common.ApiVersioning;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace eApp.CommandService.Api.Commands;

public class CommandsModule() : CarterModule("/api/platforms/{platformId:required}/commands")
{
    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        var platformGroup = app.MapGroup("")
            .WithApiVersionSet(ApiVersions.ApiVersionSet)
            .MapToApiVersion(ApiVersions.V1);
        
        platformGroup.MapGet("", async ([FromRoute] int platformId, ISender sender) =>
        {
            var result = await sender.Send(new GetCommandsForPlatformIdQuery(platformId));
            return result.IsFailure ? Results.BadRequest() : Results.Ok(result.Value);
        });

        platformGroup.MapGet("{commandId:required}", async ([FromRoute] int platformId, [FromRoute] int commandId, ISender sender) =>
        {
            var result = await sender.Send(new GetCommandByIdForPlatformIdQuery(platformId, commandId));
            return result.IsFailure ? Results.BadRequest() : Results.Ok(result.Value);
        });

        platformGroup.MapPost("", async ([FromRoute] int platformId, [FromBody] CommandCreateDto commandCreateDto, ISender sender) => 
        {
            var result = await sender.Send(new CreateCommandForPlatformIdCommand(platformId, commandCreateDto));
            return result.IsFailure ? Results.BadRequest() : Results.Created($"/api/platforms/{platformId}/commands/{result.Value.Id}", result.Value);
        });
    }
}