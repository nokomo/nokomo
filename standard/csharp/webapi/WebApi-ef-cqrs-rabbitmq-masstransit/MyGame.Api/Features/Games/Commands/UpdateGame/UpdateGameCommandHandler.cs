using MassTransit;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MyGame.Api.Data;
using NyGame.Contracts.Dtos;
using NyGame.Contracts.Models;

namespace MyGame.Api.Features.Games.Commands.UpdateGame;

public record UpdateGameCommandHandler(MyGameContext Context, IPublishEndpoint PublishEndpoint) : IRequestHandler<UpdateGameCommand, UpdateGameDto?>
{
  public async Task<UpdateGameDto?> Handle(UpdateGameCommand request, CancellationToken cancellationToken)
  {
    var game = await Context.Games.FirstOrDefaultAsync(g => g.Id == request.Id, cancellationToken);
    if (game is null) return null;

    game.Name = request.Name;
    game.GenreId = request.GenreId;
    game.Price = request.Price;
    game.ReleaseDate = request.ReleaseDate;
    Context.Games.Update(game);
    await PublishEndpoint.Publish(game.ToGameUpdated());
    await Context.SaveChangesAsync(cancellationToken);

    return game.ToUpdateGameDto();
  }
}
