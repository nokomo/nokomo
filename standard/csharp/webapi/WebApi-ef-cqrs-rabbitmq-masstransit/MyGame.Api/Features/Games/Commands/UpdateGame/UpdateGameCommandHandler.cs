using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyGame.Data;
using MyGame.Dtos;
using MyGame.Models;

namespace MyGame.Features.Games.Commands.UpdateGame;

public record UpdateGameCommandHandler(MyGameContext context) : IRequestHandler<UpdateGameCommand, UpdateGameDto?>
{
  public async Task<UpdateGameDto?> Handle(UpdateGameCommand request, CancellationToken cancellationToken)
  {
    var game = await context.Games.FirstOrDefaultAsync(g => g.Id == request.Id, cancellationToken);
    if (game is null) return null;

    game.Name = request.Name;
    game.GenreId = request.GenreId;
    game.Price = request.Price;
    game.ReleaseDate = request.ReleaseDate;
    context.Games.Update(game);
    await context.SaveChangesAsync(cancellationToken);

    return game.ToUpdateGameDto();
  }
}
