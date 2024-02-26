using eApp.Common.Configs;

namespace eApp.CommandService.Api;

public class AppConfig
{
    public const string Section = "AppConfig";
    
    public string PlatformServiceUrl { get; init; }
    public string PlatformServiceGrpcUrl { get; init; }
}

public class ConnectionStringsConfig
{
    public const string Section = "ConnectionStrings";
    
    public string CommandMssql { get; init; }
}

public class GrpcConfg
{
    public const string Section = "Grpc";
    
    public string GetPlatformProtoPath { get; init; }
}