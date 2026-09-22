namespace MyGame.Features.Games.Queries.GetGames;

public record GameFilter
{
  public string? Search { get; init; }
  public decimal? MinPrice { get; init; }
  public decimal? MaxPrice { get; init; }

  //pagination

  public int PageNumber { get; init; } = 1;
  public int PageSize { get; init; } = 10;  
  
}
