using Asp.Versioning;
using Asp.Versioning.Builder;

namespace eApp.PlatformService.Api.Constants;

public static class ApiVersions
{
    public static readonly ApiVersion V1 = new(1, 0);
    public static readonly ApiVersion V2 = new(2, 0);
    
    public static ApiVersionSet ApiVersionSet { get; set; }
    
    public static ApiVersionSet InitializeApiVersionSet(this WebApplication app)
    {
        ApiVersionSet = app.NewApiVersionSet()
            .HasApiVersion(ApiVersions.V1)
            .HasApiVersion(ApiVersions.V2)
            .ReportApiVersions()
            .Build();

        return ApiVersionSet;
    }
}