using Carter;
using eApp.PlatformService.Api.Constants;

namespace eApp.PlatformService.Api.Platforms;

public class PlatformsModule() : CarterModule("/api/platforms")
{
    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/")
            .MapToApiVersion(ApiVersions.V1);

        group.MapGet("/", () => "Hello from Carter!");
    }
}