using MediatR;
using MyGame.Dtos;

namespace MyGame.Features.Games.Commands.CreateGame;

public record CreateGameCommand
(
  string Name,
  int GenreId,
  decimal Price,
  DateOnly ReleaseDate
) : IRequest<GameDetailsDto>;
