using Microsoft.EntityFrameworkCore;
using MyGame.Api.Data;
using NyGame.Contracts.Dtos;
using NyGame.Contracts.Models;

namespace MyGame.Services;

public class GameService(MyGameContext context) : IGameService
{

  public async Task<IEnumerable<GameSummaryDto>> GetGamesAsync()
  {
    var games = await context.Games.Include(g => g.Genre).ToListAsync();
    return games.Select(g => g.ToGameSummaryDto());
  }

  public async Task<GameDetailsDto?> GetGameByIdAsync(int id)
  {
    var game = await context.Games.FindAsync(id);
    return game?.ToGameDetailDto();
  }

  public async Task<GameDetailsDto> CreateGameAsync(CreateGameDto newGame)
  {
    Game game = newGame.ToGame();
    await context.Games.AddAsync(game);
    await context.SaveChangesAsync();
    return game.ToGameDetailDto();
  }

  public async Task<GameDetailsDto?> UpdateGameAsync(int id, UpdateGameDto updatedGame)
  {
    var game = await context.Games.FindAsync(id);
    if (game is null) return null;

    game = updatedGame.ToGame();
    context.Games.Update(game);
    await context.SaveChangesAsync();

    return game.ToGameDetailDto();
  }

  public async Task<bool> DeleteGameAsync(int id)
  {
    int rowsAffected = await context.Games.Where(g => g.Id == id).ExecuteDeleteAsync();
    return rowsAffected == 1;
  }
}
