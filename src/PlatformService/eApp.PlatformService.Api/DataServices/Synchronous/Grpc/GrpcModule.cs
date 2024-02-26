using Carter;
using eApp.Common.ApiVersioning;

namespace eApp.PlatformService.Api.DataServices.Synchronous.Grpc;

public class GrpcModule() : CarterModule("/api/grpc")
{
    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("");

        group.MapGet("/protos/platforms", async (context) 
                => await context.Response.SendFileAsync("Protos/platforms.proto"))
            .WithApiVersionSet(ApiVersions.ApiVersionSet)
            .MapToApiVersion(ApiVersions.V1);
    }
}