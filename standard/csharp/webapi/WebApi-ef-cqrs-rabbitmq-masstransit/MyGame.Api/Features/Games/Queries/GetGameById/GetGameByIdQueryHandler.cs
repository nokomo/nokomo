using MediatR;
using Microsoft.EntityFrameworkCore;
using MyGame.Api.Data;
using NyGame.Contracts.Dtos;
using NyGame.Contracts.Models;

namespace MyGame.Api.Features.Games.Queries.GetGameById;

public record GetGameByIdQueryHandler(MyGameContext context) : IRequestHandler<GetGameByIdQuery, GameDetailsDto?>
{
  public async Task<GameDetailsDto?> Handle(GetGameByIdQuery request, CancellationToken cancellationToken)
  {
    var game = await context.Games
        .Include(g => g.Genre).AsNoTracking()
        .FirstOrDefaultAsync(g => g.Id == request.Id, cancellationToken);

    if (game is null)
    {
      return null;
    }

    return game.ToGameDetailDto();
  }
}
