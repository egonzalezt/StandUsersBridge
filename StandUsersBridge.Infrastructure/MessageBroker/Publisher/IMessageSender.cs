namespace StandUsersBridge.Infrastructure.MessageBroker.Publisher;

using System.Collections.Generic;

public interface IMessageSender
{
    void SendMessage<T>(T message, string queueName, IDictionary<string, object>? headers = null);
}
