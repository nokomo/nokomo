using MediatR;
using NyGame.Contracts.Dtos;

namespace MyGame.Api.Features.Games.Commands.CreateGame
{
    public record CreateGameCommand
    (
      string Name,
      int GenreId,
      decimal Price,
      DateOnly ReleaseDate
    ) : IRequest<GameDetailsDto>;
}
