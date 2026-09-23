using System.ComponentModel.DataAnnotations;

namespace NyGame.Contracts.Dtos;

public record UpdateGameDto(
    [Required][MaxLength(50)] string Name,
    [Range(1, 50)] int GenreId,
    [Range(1, 100)] decimal Price,
    DateOnly ReleaseDate
);
