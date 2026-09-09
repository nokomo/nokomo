using MyGame.Dtos;
using MyGame.Services;

namespace MyGame.Endpoints;

public static class GameEndpoints
{
    const string GetGameEndpointName = "GetGame";

    public static void MapGamesEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/games");

        group.MapGet("/", GetALlGames);
        group.MapGet("/{id:int}", GetGameById);
        group.MapPost("/", CreateGame);
        group.MapPut("/{id:int}", UpdateGame);
        group.MapDelete("/{id:int}", DeleteGame);
    }

    private static async Task<IResult> GetALlGames(IGameService gameService)
    {
        var games = await gameService.GetGamesAsync();
        return Results.Ok(games);
    }

    private static async Task<IResult> GetGameById(int id, IGameService gameService)
    {
        var game = await gameService.GetGameByIdAsync(id);
        return game is not null ? Results.Ok(game) : Results.NotFound();
    }

    private static async Task<IResult> CreateGame(CreateGameDto newGame, IGameService gameService)
    {
        var game = await gameService.CreateGameAsync(newGame);
        return Results.CreatedAtRoute(GetGameEndpointName, new { id = game.Id }, game);
    }

    private static async Task<IResult> UpdateGame(int id, UpdateGameDto updatedGame, IGameService gameService)
    {
        var game = await gameService.UpdateGameAsync(id, updatedGame);
        return game is not null ? Results.Ok(game) : Results.NotFound();
    }

    private static async Task<IResult> DeleteGame(int id, IGameService gameService)
    {
        var deleted = await gameService.DeleteGameAsync(id);
        return deleted ? Results.NoContent() : Results.NotFound();
    }
}