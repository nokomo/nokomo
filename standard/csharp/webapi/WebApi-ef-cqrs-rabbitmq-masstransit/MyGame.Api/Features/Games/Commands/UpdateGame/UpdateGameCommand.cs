using MediatR;
using MyGame.Dtos;

namespace MyGame.Features.Games.Commands.UpdateGame;

public record UpdateGameCommand
(
    int Id,
    string Name,
    int GenreId,
    decimal Price,
    DateOnly ReleaseDate
) : IRequest<UpdateGameDto?>;