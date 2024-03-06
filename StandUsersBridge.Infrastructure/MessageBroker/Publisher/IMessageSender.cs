namespace StandUsersBridge.Infrastructure.MessageBroker.Publisher;

using System.Collections.Generic;

public interface IMessageSender
{
    void SendMessage<T>(T message, string routingKey, string exchangeName, IDictionary<string, object>? headers = null);
}
