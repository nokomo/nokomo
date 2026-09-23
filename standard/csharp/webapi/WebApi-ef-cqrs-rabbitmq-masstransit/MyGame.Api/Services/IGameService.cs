using NyGame.Contracts.Dtos;

namespace MyGame.Services
{
  public interface IGameService
  {
    Task<IEnumerable<GameSummaryDto>> GetGamesAsync();
    Task<GameDetailsDto?> GetGameByIdAsync(int id);
    Task<GameDetailsDto> CreateGameAsync(CreateGameDto newGame);
    Task<GameDetailsDto?> UpdateGameAsync(int id, UpdateGameDto updatedGame);
    Task<bool> DeleteGameAsync(int id);
  }
}
