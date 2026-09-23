using MediatR;
using MyGame.Api.Dtos;
using NyGame.Contracts.Dtos;

namespace MyGame.Features.Games.Queries.GetGames;

public record GetGamesQuery(GameFilter Filter) : IRequest<PagedResult<GameSummaryDto>>;
