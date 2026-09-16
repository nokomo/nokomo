using System.ComponentModel.DataAnnotations;

namespace MyGame.Dtos;

public record CreateGameDto(
    [Required][MaxLength(50)] string Name,
    [Range(1, 50)] int GenreId,
    [Range(1, 100)] decimal Price,
    DateOnly ReleaseDate
);
