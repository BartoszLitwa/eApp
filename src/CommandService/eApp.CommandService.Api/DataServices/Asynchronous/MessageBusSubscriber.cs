using System.Text;
using eApp.CommandService.Api.EventProcessing;
using eApp.Common.Configs;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace eApp.CommandService.Api.DataServices.Asynchronous;

public class MessageBusSubscriber : BackgroundService
{
    private readonly IEventProcessor _eventProcessor;
    private readonly ILogger<MessageBusSubscriber> _logger;
    private readonly IConnection _connection;
    private readonly IChannel _channel;
    private readonly string _queueName;

    public MessageBusSubscriber(IOptions<RabbitMqConfig> rabbitMqConfig, IEventProcessor eventProcessor, ILogger<MessageBusSubscriber> logger)
    {
        _eventProcessor = eventProcessor;
        _logger = logger;
        var factory = new ConnectionFactory()
        {
            HostName = rabbitMqConfig.Value.Host,
            Port = rabbitMqConfig.Value.Port,
            DispatchConsumersAsync = true
        };

        _connection = factory.CreateConnection();
        _channel = _connection.CreateChannel();
        
        _channel.ExchangeDeclare(exchange: "platform.exchange.topic", type: ExchangeType.Topic);
        _queueName = _channel.QueueDeclare().QueueName;
        // Using wildcard for now - # = 0 or more words
        _channel.QueueBind(queue: _queueName, exchange: "platform.exchange.topic", routingKey: "platform.*");

        Console.WriteLine("--> Listening on the message bus...");
        
        _connection.ConnectionShutdown += RabbitMQ_ConnectionShutdown;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        stoppingToken.ThrowIfCancellationRequested();

        var consumer = new AsyncEventingBasicConsumer(_channel);
        consumer.Received += async (moduleHandle, ea) =>
        {
            Console.WriteLine("--> Event received!");
            var body = ea.Body;
            var notificationMessage = Encoding.UTF8.GetString(body.ToArray());

            var cancellationToken = new CancellationToken();
            await _eventProcessor.ProcessEvent(notificationMessage, cancellationToken);
        };

        await _channel.BasicConsumeAsync(queue: _queueName, autoAck: true, consumer: consumer);
    }

    public override void Dispose()
    {
        if (_channel.IsOpen)
        {
            _channel.Close();
            _connection.Close();
        }
        
        base.Dispose();
    }

    private void RabbitMQ_ConnectionShutdown(object sender, ShutdownEventArgs e)
    {
        _logger.LogInformation("--> RabbitMQ Connection Shutdown");
    }
}