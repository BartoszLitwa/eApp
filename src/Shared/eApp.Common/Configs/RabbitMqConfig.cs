namespace eApp.Common.Configs;

public class RabbitMqConfig
{
    public const string Section = "RabbitMq";
    
    public string Host { get; init; }
    public int Port { get; init; }
}