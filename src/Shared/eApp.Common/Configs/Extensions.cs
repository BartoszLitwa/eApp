using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace eApp.Common.Configs;

public static class Extensions
{
    public static void AddRabbitMqConfig(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<RabbitMqConfig>(configuration.GetSection(RabbitMqConfig.Section));
    }
}