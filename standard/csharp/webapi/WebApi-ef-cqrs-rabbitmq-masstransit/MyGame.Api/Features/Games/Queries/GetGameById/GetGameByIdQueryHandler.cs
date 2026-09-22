using MediatR;
using Microsoft.EntityFrameworkCore;
using MyGame.Data;
using MyGame.Dtos;
using MyGame.Models;

namespace MyGame.Features.Games.Queries.GetGameById;

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
