using MassTransit;
using Microsoft.Extensions.Options;
using MyGame.Api.Data;
using NyGame.Contracts.Messaging;

namespace MyGame.Api.Messaging;

public static class MassTransitConfigurations
{
    public static void AddMassTransitConfigurations(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<RabbitMqOptions>(configuration.GetSection("RabbitMq"));
        services.AddMassTransit(x =>
        {
            //EF Transactional outbox pattern
            x.AddEntityFrameworkOutbox<MyGameContext>(o =>
            {
                o.UsePostgres();
                o.UseBusOutbox();
            });
            x.UsingRabbitMq((context, cfg) =>
            {
                var rabbitMqOptions = context.GetRequiredService<IOptions<RabbitMqOptions>>().Value;
                cfg.Host(rabbitMqOptions.Host, "/",h =>
                {
                    h.Username(rabbitMqOptions.UserName);
                    h.Password(rabbitMqOptions.Password);
                });
            });
        });
    }
}
