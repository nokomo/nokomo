using MassTransit;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MyGame.Api.Data;
using NyGame.Contracts.Models;

namespace MyGame.Api.Features.Games.Commands.DeleteGame;

public record DeleteGameCommandHandler(MyGameContext Context, IPublishEndpoint PublishEndpoint) : IRequestHandler<DeleteGameCommand, bool>
{
  public async Task<bool> Handle(DeleteGameCommand request, CancellationToken cancellationToken)
  {
    var game = await Context.Games.FirstOrDefaultAsync(g => g.Id == request.Id, cancellationToken);
    if (game is null) return false;
    Context.Games.Remove(game);
    await PublishEndpoint.Publish(game.ToGameDeleted());
    await Context.SaveChangesAsync(cancellationToken);
    return true;
  }
}
