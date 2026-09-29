using MassTransit;
using MediatR;
using MyGame.Api.Data;
using NyGame.Contracts.Dtos;
using NyGame.Contracts.Models;

namespace MyGame.Api.Features.Games.Commands.CreateGame;

public record class CreateGameCommandHandler(MyGameContext Context, IPublishEndpoint PublishEndpoint) : IRequestHandler<CreateGameCommand, GameDetailsDto>
{
  public async Task<GameDetailsDto> Handle(CreateGameCommand request, CancellationToken cancellationToken)
  {
    var game = new Game
    {
      Name = request.Name,
      GenreId = request.GenreId,
      Price = request.Price,
      ReleaseDate = request.ReleaseDate
    };

    await Context.Games.AddAsync(game, cancellationToken);
    await PublishEndpoint.Publish(game.ToGameCreated());
    await Context.SaveChangesAsync(cancellationToken);

    return game.ToGameDetailDto();
  }
}
