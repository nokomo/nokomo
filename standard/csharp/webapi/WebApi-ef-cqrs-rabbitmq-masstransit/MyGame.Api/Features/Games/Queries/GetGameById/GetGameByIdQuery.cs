using MediatR;
using NyGame.Contracts.Dtos;

namespace MyGame.Api.Features.Games.Queries.GetGameById;
  
public record GetGameByIdQuery
(
    int Id
) : IRequest<GameDetailsDto?>;
