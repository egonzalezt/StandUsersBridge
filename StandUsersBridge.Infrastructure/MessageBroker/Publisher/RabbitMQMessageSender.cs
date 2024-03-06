namespace StandUsersBridge.Infrastructure.MessageBroker.Publisher;

using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

public class RabbitMQMessageSender : IMessageSender
{
    private readonly ConnectionFactory _connectionFactory;
    private readonly ILogger<RabbitMQMessageSender> _logger;
    public RabbitMQMessageSender(ConnectionFactory connectionFactory, ILogger<RabbitMQMessageSender> logger)
    {
        _connectionFactory = connectionFactory;
        _logger = logger;
    }

    public void SendMessage<T>(T message, string routingKey, string exchangeName, IDictionary<string, object>? headers = null)
    {
        using var connection = _connectionFactory.CreateConnection();
        using var channel = connection.CreateModel();
        channel.ExchangeDeclare(exchange: exchangeName, type: ExchangeType.Topic);
        var jsonMessage = JsonSerializer.Serialize(message);
        var body = Encoding.UTF8.GetBytes(jsonMessage);
        var properties = channel.CreateBasicProperties();
        properties.Persistent = true;
        properties.ContentType = "application/json";
        if (headers != null)
        {
            properties.Headers = headers;
        }

        channel.BasicPublish(exchange: exchangeName,
                             routingKey: routingKey,
                             basicProperties: properties,
                             body: body);
        _logger.LogInformation($"Message send to '{exchangeName}' with routing key: '{routingKey}'");
    }
}

