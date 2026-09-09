
using MyGame.Dtos;

namespace MyGame.Endpoints;

public static class GameEndpoints
{
    const string GetGameEndpointName = "GetGame";
    private static List<GameDto> games =
[
    new (1, "Game 1", "Action", 49.99m, new DateOnly(2023, 1, 15)),
    new (2, "Game 2", "Adventure", 59.99m, new DateOnly(2023, 6, 20)),
    new (3, "Game 3", "Puzzle", 39.99m, new DateOnly(2023, 3, 10))
];

    public static void MapGamesEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/games");

        group.MapGet("/", () =>
        {
            return Results.Ok(games);
        });

        group.MapGet("/{id:int}", (int id) =>
        {
            var game = games.FirstOrDefault(g => g.Id == id);
            return game is not null ?
                Results.Ok(game) :
                Results.NotFound();
        }).WithName(GetGameEndpointName);

        group.MapPost("/", (CreateGameDto newGame) =>
        {
            var nextId = games.Max(g => g.Id) + 1;
            var game = new GameDto(
                nextId,
                newGame.Name,
                newGame.Genre,
                newGame.Price,
                newGame.ReleaseDate
           );
            games.Add(game);
            return Results.CreatedAtRoute(GetGameEndpointName, new { id = nextId }, game);
        });

        group.MapPut("/{id:int}", (int id, UpdateGameDto updatedGame) =>
        {
            var index = games.FindIndex(g => g.Id == id);
            if (index == -1)
            {
                return Results.NotFound();
            }
            games[index] = new GameDto(
                games[index].Id,
                updatedGame.Name,
                updatedGame.Genre,
                updatedGame.Price,
                updatedGame.ReleaseDate
            );
            return Results.NoContent();
        });

        group.MapDelete("/{id:int}", (int id) =>
        {
            games.RemoveAll(g => g.Id == id);
            return Results.NoContent();
        });
    }
}