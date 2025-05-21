using System.Reflection;
using MassTransit;
using Projeli.Shared.Application.Messages.Files;
using Projeli.Shared.Infrastructure.Exceptions;
using Projeli.StorageService.Api.Consumers;

namespace Projeli.StorageService.Api.Extensions;

public static class RabbitMqExtension
{
    public static void UseStorageServiceRabbitMq(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMassTransit(x =>
        {
            x.AddConsumer<FileStoreConsumer>();
            x.AddConsumer<FileDeleteConsumer>();
            
            x.UsingRabbitMq((context, config) =>
            {
                config.Host(configuration["RabbitMq:Host"] ?? throw new MissingEnvironmentVariableException("RabbitMq:Host"), "/", h =>
                {
                    h.Username(configuration["RabbitMq:Username"] ?? throw new MissingEnvironmentVariableException("RabbitMq:Username"));
                    h.Password(configuration["RabbitMq:Password"] ?? throw new MissingEnvironmentVariableException("RabbitMq:Password"));
                });
                
                config.ReceiveEndpoint("storage-file-store-queue", e =>
                {
                    e.ConfigureConsumer<FileStoreConsumer>(context);
                });
                
                config.ReceiveEndpoint("storage-file-delete-queue", e =>
                {
                    e.ConfigureConsumer<FileDeleteConsumer>(context);
                });
                
                config.PublishFanOut<FileStoreFailedMessage>();
                config.PublishFanOut<FileStoredMessage>();
            });
        });
    }

    private static void PublishFanOut<T>(this IRabbitMqBusFactoryConfigurator configurator)
        where T : class
    {
        configurator.Publish<T>(y => y.ExchangeType = "fanout");
    }
}