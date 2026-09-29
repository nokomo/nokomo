using System;
using MassTransit;
using NyGame.Contracts.Events;

namespace MyGame.Worker.Consumers;

public class GameDeletedConsumer(ILogger<GameDeletedConsumer> logger) : IConsumer<GameDeleted>
{
    public Task Consume(ConsumeContext<GameDeleted> context)
    {
        var message = context.Message;
        logger.LogInformation("Game deleted: {Id}", message.Id);
        return Task.CompletedTask;
    }
}
