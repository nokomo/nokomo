using MyGame.Dtos;
using MyGame.Data;
using MyGame.Models;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

namespace MyGame.Services;

public class GameService(MyGameContext context, IMapper mapper) : IGameService
{

  public async Task<IEnumerable<GameSummaryDto>> GetGamesAsync()
  {
    var games = await context.Games.Include(g => g.Genre).ToListAsync();
    return games.Select(g => mapper.Map<GameSummaryDto>(g));
  }

  public async Task<GameDetailsDto?> GetGameByIdAsync(int id)
  {
    var game = await context.Games.FindAsync(id);
    return game is not null ? mapper.Map<GameDetailsDto>(game) : null;
  }

  public async Task<GameDetailsDto> CreateGameAsync(CreateGameDto newGame)
  {
    Game game = new()
    {
      Name = newGame.Name,
      GenreId = newGame.GenreId,
      Price = newGame.Price,
      ReleaseDate = newGame.ReleaseDate
    };
    await context.Games.AddAsync(game);
    await context.SaveChangesAsync();
    return mapper.Map<GameDetailsDto>(game);
  }

  public async Task<GameDetailsDto?> UpdateGameAsync(int id, UpdateGameDto updatedGame)
  {
    var game = await context.Games.FindAsync(id);
    if (game is null) return null;

    mapper.Map(updatedGame, game);
    context.Games.Update(game);
    await context.SaveChangesAsync();

    return mapper.Map<GameDetailsDto>(game);
  }

  public async Task<bool> DeleteGameAsync(int id)
  {
    int rowsAffected = await context.Games.Where(g => g.Id == id).ExecuteDeleteAsync();
    return rowsAffected == 1;
  }
}
