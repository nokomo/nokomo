namespace NyGame.Contracts.Events;

public record class GameUpdated
(
    int Id,
    string Name,
    int GenreId,
    decimal Price,
    DateOnly ReleaseDate
);
