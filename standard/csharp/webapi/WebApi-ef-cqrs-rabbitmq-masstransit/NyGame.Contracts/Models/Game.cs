using System;
using System.ComponentModel.DataAnnotations;
using NyGame.Contracts.Dtos;

namespace NyGame.Contracts.Models;

public class Game
{
    [Key]
     public int Id { get; set; }
    
    [MinLength (1)]
    [MaxLength (50)]
    public required string Name { get; set; }
    
    [MinLength (3)]
    [MaxLength (100)]
    public Genre? Genre { get; set; }
    public int GenreId { get; set; }
    
    [Required]
    [Range (0,100000)]
    public decimal Price { get; set; }
    public DateOnly ReleaseDate { get; set; }
}
