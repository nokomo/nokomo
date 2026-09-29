using MassTransit;
using Microsoft.Extensions.Options;
using MyGame.Worker.Consumers;
using NyGame.Contracts.Messaging;

namespace MyGame.Worker.Messagging;

public static class MassTransitConfigurations
{
    public static void AddMassTransitConfigurations(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<RabbitMqOptions>(configuration.GetSection("RabbitMq"));
        services.AddMassTransit(x =>
        {
            x.AddConsumer<GameCreatedConsumer>();
            x.AddConsumer<GameDeletedConsumer>();
            x.AddConsumer<GameUpdatedConsumer>();
            x.UsingRabbitMq((context, cfg) =>
            {
                var rabbitMqOptions = context.GetRequiredService<IOptions<RabbitMqOptions>>().Value;
                cfg.Host(rabbitMqOptions.Host, h =>
                {
                   h.Username(rabbitMqOptions.UserName);
                   h.Password(rabbitMqOptions.Password);
                });
                // Retries a failed message 3 times, 5 seconds apart,
                // before sending it to the _error queue.
                cfg.UseMessageRetry(r => r.Interval(3, TimeSpan.FromSeconds(5)));
                cfg.ConfigureEndpoints(context);
            });
        });
    }
}
