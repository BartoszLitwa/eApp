using Asp.Versioning;
using Asp.Versioning.Builder;
using Microsoft.AspNetCore.Builder;

namespace eApp.Common.ApiVersioning;

public static class ApiVersions
{
    public static readonly ApiVersion V1 = new(1, 0);
    public static readonly ApiVersion V2 = new(2, 0);
    
    public static ApiVersionSet ApiVersionSet { get; set; }
}