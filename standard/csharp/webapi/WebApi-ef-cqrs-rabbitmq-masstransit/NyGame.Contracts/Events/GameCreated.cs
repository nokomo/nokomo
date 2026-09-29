using System;
using NyGame.Contracts.Models;

namespace NyGame.Contracts.Events;

public record GameCreated
(
    int Id,
    string Name,
    int GenreId,
    decimal Price,
    DateOnly ReleaseDate
);
