using Carter;
using eApp.CommandService.Api.Platforms.Queries;
using eApp.Common.ApiVersioning;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace eApp.CommandService.Api.Platforms;

public class PlatformsModule() : CarterModule("/api/platforms")
{
    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("");

        group.MapGet("/", async (ISender sender) =>
        {
            var result = await sender.Send(new GetPlatformsQuery());
            return result.IsFailure ? Results.BadRequest() : Results.Ok(result.Value);
        });
        
        group.MapGet("/{id:required}", async ([FromRoute] int id, ISender sender) =>
        {
            var result = await sender.Send(new GetPlatformByIdQuery(id));
            return result.IsFailure ? Results.BadRequest() : Results.Ok(result.Value);
        });
        
        group.MapPost("/", (ISender sender) =>
        {
            Console.WriteLine("Inbound POST - Command Service");
            return Results.Ok("Inbound POST - Command Service");
        }).WithApiVersionSet(ApiVersions.ApiVersionSet)
            .MapToApiVersion(ApiVersions.V1);
    }
}