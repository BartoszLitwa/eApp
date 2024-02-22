using eApp.Common.Configs;

namespace eApp.PlatformService.Api;

public class AppConfig
{
    public const string Section = "AppConfig";

    public string CommandServiceUrl { get; init; }
}

public class ConnectionStringsConfig
{
    public const string Section = "ConnectionStrings";
    
    public string PlatformMssql { get; init; }
}