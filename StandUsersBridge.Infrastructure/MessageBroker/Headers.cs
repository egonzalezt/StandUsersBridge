namespace StandUsersBridge.Infrastructure.MessageBroker;

using System;
using System.Collections.Generic;

public class Headers(string messageType)
{
    public Guid MessageId { get; set; } = Guid.NewGuid();
    public string MessageType { get; set; } = messageType;
    public string Timestamp { get; set; } = DateTimeOffset.UtcNow.ToString("o");
    public int Priority { get; set; } = 1;

    public Dictionary<string, object> GetAttributesAsDictionary()
    {
        var dictionary = new Dictionary<string, object>();

        foreach (var property in GetType().GetProperties())
        {
            var value = property.PropertyType == typeof(Guid) ? property.GetValue(this).ToString() : property.GetValue(this);
            dictionary[property.Name] = value;
        }

        return dictionary;
    }
}
