namespace eApp.PlatformService.Api;

public class AppConfig
{
    public const string Section = "AppConfig";

    public string CommandServiceUrl { get; init; }
    
    public ConnectionStringsConfig ConnectionStrings { get; init; }
    public RabbitMqConfig RabbitMq { get; init; }
}

public class ConnectionStringsConfig
{
    public const string Section = "ConnectionStrings";
    
    public string PlatformMssql { get; init; }
}

public class RabbitMqConfig
{
    public const string Section = "RabbitMq";
    
    public string Host { get; init; }
    public int Port { get; init; }
}