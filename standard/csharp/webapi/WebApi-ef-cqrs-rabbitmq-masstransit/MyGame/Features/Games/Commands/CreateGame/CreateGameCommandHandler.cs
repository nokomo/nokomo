using MediatR;
using MyGame.Data;
using MyGame.Dtos;
using MyGame.Models;

namespace MyGame.Features.Games.Commands.CreateGame;

public record class CreateGameCommandHandler(MyGameContext Context) : IRequestHandler<CreateGameCommand, GameDetailsDto>
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
    await Context.SaveChangesAsync(cancellationToken);

    return game.ToGameDetailDto();
  }
}
