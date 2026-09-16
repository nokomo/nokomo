namespace MyGame.Dtos;

public record GameSummaryDto(
    int Id,
    string Name,
    string GenreName,
    decimal Price,
    DateOnly ReleaseDate
);
