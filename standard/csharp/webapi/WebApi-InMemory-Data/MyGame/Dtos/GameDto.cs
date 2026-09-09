namespace MyGame.Dtos;

// A DTO is a contract between the client and the server since it represents 
// a shared agreement about how data wull be transferred and used.

public record  GameDto(
    int Id,
    string Name,
    string Genre,
    decimal price,
    DateOnly ReleaseDate
);
