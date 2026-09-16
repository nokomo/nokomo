using MediatR;
using Microsoft.EntityFrameworkCore;
using MyGame.Data;
using MyGame.Dtos;
using MyGame.Models;

namespace MyGame.Features.Games.Queries.GetGames
{
  public record class GetGamesQueryHandler(MyGameContext context) : IRequestHandler<GetGamesQuery, PagedResult<GameSummaryDto>>
  {
    public async Task<PagedResult<GameSummaryDto>> Handle(GetGamesQuery request, CancellationToken cancellationToken)
    {
      var filter = request.Filter;
      var query = context.Games.AsQueryable();
      if (!string.IsNullOrEmpty(filter.Search))
      {
        query = query.Where(g => g.Name.Contains(filter.Search));
      }
      if (filter.MinPrice.HasValue)
      {
        query = query.Where(g => g.Price >= filter.MinPrice.Value);
      }
      if (filter.MaxPrice.HasValue)
      {
        query = query.Where(g => g.Price <= filter.MaxPrice.Value);
      }
      var totalCount = await query.CountAsync(cancellationToken);
      var games = await query
        .Include(g => g.Genre)
        .OrderBy(g => g.Id)
        .Skip((filter.PageNumber - 1) * filter.PageSize)
        .Take(filter.PageSize)
        .AsNoTracking()
        .ToListAsync(cancellationToken);
      return new PagedResult<GameSummaryDto>
      {
        Items = games.Select(g => g.ToGameSummaryDto()),
        TotalCount = totalCount,
        PageNumber = filter.PageNumber,
        PageSize = filter.PageSize
      };
    }
  }
}
