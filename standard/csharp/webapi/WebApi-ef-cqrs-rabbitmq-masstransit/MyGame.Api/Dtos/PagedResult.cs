namespace MyGame.Dtos;

public class PagedResult<T>
{
  public IEnumerable<T> Items { get; init; } = [];
  public int TotalCount { get; init; }
  public int PageNumber { get; set; }
  public int PageSize { get; set; }
  public int TotalPages => (int)Math.Ceiling((double)TotalCount / PageSize);
}
