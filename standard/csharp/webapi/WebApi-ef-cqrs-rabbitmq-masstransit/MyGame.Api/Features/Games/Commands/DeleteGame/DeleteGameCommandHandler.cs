using MediatR;
using Microsoft.EntityFrameworkCore;
using MyGame.Api.Data;

namespace MyGame.Api.Features.Games.Commands.DeleteGame;

public record DeleteGameCommandHandler(MyGameContext Context) : IRequestHandler<DeleteGameCommand, bool>
{
  public async Task<bool> Handle(DeleteGameCommand request, CancellationToken cancellationToken)
  {
    var game = await Context.Games.FirstOrDefaultAsync(g => g.Id == request.Id, cancellationToken);
    if (game is null) return false;
    Context.Games.Remove(game);
    await Context.SaveChangesAsync(cancellationToken);
    return true;
  }
}
