namespace MyGame.Dtos;

// A DTO is a contract between the client and the server since it represents 
// a shared agreement about how data wull be transferred and used.

public record GameDetailsDto(
    int Id,
    string Name,
    int GenreId,
    decimal Price,
    DateOnly ReleaseDate
);
