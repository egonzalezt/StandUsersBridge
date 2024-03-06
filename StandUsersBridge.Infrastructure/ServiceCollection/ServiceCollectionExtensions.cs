namespace StandUsersBridge.Infrastructure.ServiceCollection;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;
using MessageBroker.Configuration;
using MessageBroker.Publisher;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var rabbitMqConfigurationFactory = new ConnectionFactory();
        configuration.GetSection("RabbitMQ:Publisher").Bind(rabbitMqConfigurationFactory);
        services.AddSingleton(rabbitMqConfigurationFactory);
        services.Configure<StandUsersExchange>(configuration.GetSection("RabbitMQ:StandUsersExchange"));
        services.AddSingleton<IMessageSender, RabbitMQMessageSender>();

        return services;
    }
}
