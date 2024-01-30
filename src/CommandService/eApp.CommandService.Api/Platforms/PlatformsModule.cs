using Carter;
using eApp.Common.ApiVersioning;
using MediatR;

namespace eApp.CommandService.Api.Platforms;

public class PlatformsModule() : CarterModule("/api/platforms")
{
    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("");
        
        group.MapPost("/", async (ISender sender) =>
        {
            Console.WriteLine("Inbound POST - Command Service");
            return Results.Ok("Inbound POST - Command Service");
        }).WithApiVersionSet(ApiVersions.ApiVersionSet)
            .MapToApiVersion(ApiVersions.V1);
    }
}