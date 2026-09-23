using MediatR;
using Microsoft.AspNetCore.Mvc;
using MyGame.Features.Games.Queries.GetGames;
using MyGame.Api.Features.Games.Commands.DeleteGame;
using MyGame.Api.Features.Games.Commands.UpdateGame;
using MyGame.Api.Features.Games.Queries.GetGameById;
using MyGame.Api.Features.Games.Commands.CreateGame;

namespace MyGame.Endpoints;

public static class GameEndpoints
{
    private const string GetGameEndpointName = "GetGameById";

    public static void MapGamesEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/games");

        group.MapGet("/", GetGames);
        group.MapGet("/{id:int}", GetGameById).WithName(GetGameEndpointName);
        group.MapPost("/", CreateGame);
        group.MapPut("/{id:int}", UpdateGame);
        group.MapDelete("/{id:int}", DeleteGame);
    }

    private static async Task<IResult> GetGames([AsParameters] GameFilter filter, IMediator mediator)
    {
        var query = new GetGamesQuery(filter);
        var games = await mediator.Send(query);
        return Results.Ok(games);
    }

    private static async Task<IResult> GetGameById(int id, IMediator meditor)
    {
        var query = new GetGameByIdQuery(id);
        var game = await meditor.Send(query);

        return game is not null
                ? Results.Ok(game)
                : Results.NotFound($"Game with id {id} not found.");
    }

    private static async Task<IResult> CreateGame(CreateGameCommand command, IMediator mediator)
    {
        var gameDetailsDto = await mediator.Send(command);
        return Results.CreatedAtRoute(GetGameEndpointName, new { id = gameDetailsDto.Id }, gameDetailsDto);
    }


    private static async Task<IResult> UpdateGame(int id, [FromBody] UpdateGameCommand command, IMediator mediator)
    {
        if (id != command.Id)
        {
            return Results.BadRequest();
        }
        var gameUpdateDto = await mediator.Send(command);
        return gameUpdateDto is not null ? Results.Ok(gameUpdateDto) : Results.NotFound();
    }

    private static async Task<IResult> DeleteGame([AsParameters] DeleteGameCommand command, IMediator mediator)
    {
        var deleted = await mediator.Send(command);
        return deleted ? Results.NoContent() : Results.NotFound();
    }
}