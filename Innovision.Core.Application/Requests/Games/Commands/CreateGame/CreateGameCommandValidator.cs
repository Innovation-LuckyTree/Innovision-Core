using FluentValidation;

namespace Innovision.Core.Application.Requests.Games.Commands.CreateGame;

public class CreateGameCommandValidator : AbstractValidator<CreateGameCommand>
{
    public CreateGameCommandValidator()
    {
        RuleFor(o => o.Name)
            .NotEmpty();

        RuleFor(o => o.Description)
            .NotEmpty();
    }
}
