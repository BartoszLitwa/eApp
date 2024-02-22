using eApp.Common.Configs;

namespace eApp.CommandService.Api;

public class AppConfig
{
    public const string Section = "AppConfig";
    
    public RabbitMqConfig RabbitMq { get; init; }
}

public class ConnectionStringsConfig
{
    public const string Section = "ConnectionStrings";
    
    public string CommandMssql { get; init; }
}