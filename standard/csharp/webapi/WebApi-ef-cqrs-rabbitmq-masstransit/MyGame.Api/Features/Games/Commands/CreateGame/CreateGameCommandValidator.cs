using FluentValidation;

namespace MyGame.Api.Features.Games.Commands.CreateGame;

public class CreateGameCommandValidator : AbstractValidator<CreateGameCommand>
{
  /*
[Required][MaxLength(50)] string Name,
    [Range(1, 50)] int GenreId,
    [Range(1, 100)] decimal Price,
  */
  public CreateGameCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required.")
            .MaximumLength(50).WithMessage("Name cannot exceed 50 characters.");

        RuleFor(x => x.Price)
            .GreaterThanOrEqualTo(1)
            .LessThanOrEqualTo(100)
            .WithMessage("Price must be between 1 and 100.");

        RuleFor(x => x.GenreId)
            .GreaterThanOrEqualTo(1)
            .LessThanOrEqualTo(50)
            .WithMessage("GenreId must be between 1 and 50.");

    }
}
