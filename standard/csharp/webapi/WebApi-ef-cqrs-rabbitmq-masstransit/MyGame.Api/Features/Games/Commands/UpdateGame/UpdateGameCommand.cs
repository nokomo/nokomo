using MediatR;
using NyGame.Contracts.Dtos;

namespace MyGame.Api.Features.Games.Commands.UpdateGame;

public record UpdateGameCommand
(
    int Id,
    string Name,
    int GenreId,
    decimal Price,
    DateOnly ReleaseDate
) : IRequest<UpdateGameDto?>;