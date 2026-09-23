using NyGame.Contracts.Dtos;

namespace NyGame.Contracts.Models;

public static class MappingExtensions
{
  public static GameDetailsDto ToGameDetailDto(this Game game)
  {
    return new GameDetailsDto(
      game.Id,
      game.Name,
      game.GenreId,
      game.Price,
      game.ReleaseDate);
  }

  public static Game ToGame(this GameDetailsDto gameDetailsDto)
  {
    return new Game
    {
      Id = gameDetailsDto.Id,
      Name = gameDetailsDto.Name,
      GenreId = gameDetailsDto.GenreId,
      ReleaseDate = gameDetailsDto.ReleaseDate,
      Price = gameDetailsDto.Price
    };
  }

  public static GameSummaryDto ToGameSummaryDto(this Game game)
  {
    return new GameSummaryDto(
      game.Id,
      game.Name,
      game.Genre?.Name ?? string.Empty,
      game.Price,
      game.ReleaseDate
    );
  }

  public static UpdateGameDto ToUpdateGameDto(this Game game)
  {
    return new UpdateGameDto(
      game.Name,
      game.GenreId,
      game.Price,
      game.ReleaseDate);
  }

  public static Game ToGame(this CreateGameDto createGameDto)
  {
    return new Game
    {
      Name = createGameDto.Name,
      GenreId = createGameDto.GenreId,
      ReleaseDate = createGameDto.ReleaseDate,
      Price = createGameDto.Price
    };
  }

  public static Game ToGame(this UpdateGameDto updateGameDto)
  {
    return new Game
    {
      Name = updateGameDto.Name,
      GenreId = updateGameDto.GenreId,
      ReleaseDate = updateGameDto.ReleaseDate,
      Price = updateGameDto.Price
    };
  }

}
