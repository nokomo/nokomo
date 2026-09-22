using MediatR;
using Microsoft.EntityFrameworkCore;
using MyGame.Data;

namespace MyGame.Features.Games.Commands.DeleteGame;

public record DeleteGameCommandHandler(MyGameContext context) : IRequestHandler<DeleteGameCommand, bool>
{
  public async Task<bool> Handle(DeleteGameCommand request, CancellationToken cancellationToken)
  {
    var game = await context.Games.FirstOrDefaultAsync(g => g.Id == request.Id, cancellationToken);
    if (game is null) return false;
    context.Games.Remove(game);
    await context.SaveChangesAsync(cancellationToken);
    return true;
  }
}
