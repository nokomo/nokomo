using MediatR;
using MyGame.Dtos;

namespace MyGame.Features.Games.Queries.GetGames;

public record GetGamesQuery(GameFilter Filter) : IRequest<PagedResult<GameSummaryDto>>;
