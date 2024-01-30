using Asp.Versioning.Builder;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace eApp.Common.ApiVersioning;

public static class ApiVersioningExtensions
{
        
    public static ApiVersionSet InitializeApiVersionSet(this WebApplication app)
    {
        ApiVersions.ApiVersionSet = app.NewApiVersionSet()
            .HasApiVersion(ApiVersions.V1)
            .HasApiVersion(ApiVersions.V2)
            .ReportApiVersions()
            .Build();

        return ApiVersions.ApiVersionSet;
    }
    
    public static void AddServiceApiVersioning(this IServiceCollection services)
    {
        services.AddApiVersioning(options =>
        {
            options.DefaultApiVersion = ApiVersions.V1;
            options.AssumeDefaultVersionWhenUnspecified = true;
        }).AddApiExplorer(options =>
        {
            options.GroupNameFormat = "'v'VVV";
        });
    }
}