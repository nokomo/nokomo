using System;
using FluentValidation;

namespace MyGame.Features.Games.Commands.UpdateGame;

public class UpdateGameCommandValidator : AbstractValidator<UpdateGameCommand>
{
  public UpdateGameCommandValidator()
  {
    RuleFor(x => x.Id)
        .GreaterThan(0).WithMessage("Id must be greater than 0.");

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
