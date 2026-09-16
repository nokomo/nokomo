using MediatR;
using MyGame.Dtos;

namespace MyGame.Features.Games.Queries.GetGameById;
  
public record GetGameByIdQuery
(
    int Id
) : IRequest<GameDetailsDto?>;
