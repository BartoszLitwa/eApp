using System.Text;
using System.Text.Json;
using eApp.Common.Configs;
using eApp.PlatformService.Api.Dtos.Platform;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;

namespace eApp.PlatformService.Api.DataServices.Asynchronous;

public class MessageBusClient : IMessageBusClient, IDisposable
{
    private readonly ILogger<MessageBusClient> _logger;
    private readonly IConnection _connection;
    private readonly IChannel _channel;

    public MessageBusClient(IOptions<RabbitMqConfig> rabbitMqConfig, ILogger<MessageBusClient> logger)
    {
        _logger = logger;
        var factory = new ConnectionFactory()
        { 
            HostName = rabbitMqConfig.Value.Host,
            Port = rabbitMqConfig.Value.Port
        };

        try
        {
            _connection = factory.CreateConnection();
            _channel = _connection.CreateChannel();
            
            _channel.ExchangeDeclare(exchange: "platform.exchange.topic", type: ExchangeType.Topic);
            
            _connection.ConnectionShutdown += RabbitMQ_ConnectionShutdown;
            _logger.LogInformation("--> Connected to message bus");
        }
        catch (Exception ex)
        {
            _logger.LogError("--> Could not connect to the message bus: {message}", ex.Message);
        }
    }

    public async ValueTask PublishNewPlatform(PlatformPublishedDto platformPublishedDto, CancellationToken cancellationToken)
    {
        var message = JsonSerializer.Serialize(platformPublishedDto);

        if (!_connection.IsOpen)
        {
            _logger.LogError("--> Connection is closed, not able to send message to the queue");
            return;
        }

        await SendMessage(message);
        _logger.LogInformation("--> RabbitMQ message sent: {message}", message);
    }

    private async Task SendMessage(string message)
    {
        var body = Encoding.UTF8.GetBytes(message);
        var properties = new BasicProperties();
        
        await _channel.BasicPublishAsync(exchange: "platform.exchange.topic",
            routingKey: "platform.new",
            basicProperties: properties,
            body: body,
            mandatory: false);
    }

    public void Dispose()
    {
        if (_channel.IsOpen)
        {
            _channel.Close();
            _connection.Close();
        }
        
        _channel.Dispose();
        _connection.Dispose();
    }

    private void RabbitMQ_ConnectionShutdown(object sender, ShutdownEventArgs e)
    {
        _logger.LogInformation("--> RabbitMQ Connection Shutdown");
    }
}