using System;
using MassTransit;
using NyGame.Contracts.Events;

namespace MyGame.Worker.Consumers;

public class GameCreatedConsumer(ILogger<GameCreated> logger) : IConsumer<GameCreated>
{
    public Task Consume(ConsumeContext<GameCreated> context)
    {
        var message = context.Message;
        logger.LogInformation("Game created: {Id}, {Name}, {GenreId}, {Price}",
            message.Id, message.Name, message.GenreId, message.Price);
        return Task.CompletedTask;
    }
}
