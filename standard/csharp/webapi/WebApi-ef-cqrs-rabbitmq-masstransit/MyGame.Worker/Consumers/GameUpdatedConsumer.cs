using MassTransit;
using NyGame.Contracts.Events;

namespace MyGame.Worker.Consumers;

public class GameUpdatedConsumer (ILogger<GameUpdatedConsumer> Logger) : IConsumer<GameUpdated>
{
    public Task Consume(ConsumeContext<GameUpdated> context)
    {
        var message = context.Message;
        Logger.LogInformation("Game updated: {Id}, {Name}, {GenreId}, {Price}",
            message.Id, message.Name, message.GenreId, message.Price);
        return Task.CompletedTask;
    }
}
