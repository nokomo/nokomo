using MediatR;

namespace MyGame.Api.Features.Games.Commands.DeleteGame;

public record class DeleteGameCommand
(
    int Id
) : IRequest<bool>;
