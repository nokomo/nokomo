using Microsoft.EntityFrameworkCore;
using MyGame.Data;
using MyGame.Dtos;

namespace MyGame.Endpoints;

public static class GenresEndpoints
{
  public static void MapGenresEndpoints(this WebApplication app)
  {
    var group = app.MapGroup("/genres");

    // GET /genres
    group.MapGet("/", async (MyGameContext context) =>
    {
      return await context.Genres
              .Select(g => new GenreDto(
                  g.Id,
                  g.Name
              ))
              .AsNoTracking()
              .ToListAsync();
    });
  }

}
