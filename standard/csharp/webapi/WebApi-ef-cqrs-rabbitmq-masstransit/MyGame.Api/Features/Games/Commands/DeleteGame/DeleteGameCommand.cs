using MediatR;

namespace MyGame.Features.Games.Commands.DeleteGame;

public record class DeleteGameCommand
(
    int Id
) : IRequest<bool>;
