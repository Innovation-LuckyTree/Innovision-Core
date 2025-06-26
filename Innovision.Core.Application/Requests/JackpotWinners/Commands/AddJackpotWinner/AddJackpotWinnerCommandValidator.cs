using FluentValidation;

namespace Innovision.Core.Application.Requests.JackpotWinners.Commands.AddJackpotWinner;

public class AddJackpotWinnerCommandValidator : AbstractValidator<AddJackpotWinnerCommand>
{
    public AddJackpotWinnerCommandValidator()
    {
        RuleFor(o => o.AccountInfoId)
            .GreaterThan(0);

        RuleFor(o => o.DrawResultId)
            .GreaterThan(0);

        RuleFor(o => o.GameId)
            .GreaterThan(0);

        RuleFor(o => o.BetTransactionId)
            .GreaterThan(0);

        RuleFor(o => o.GameScheduleId)
            .GreaterThan(0);

        RuleFor(o => o.PrizePoolAmount)
            .GreaterThan(0);

        RuleFor(o => o.GrossWinAmount)
            .GreaterThan(0);

        RuleFor(o => o.BetValue)
            .NotEmpty()
            .NotNull();

        RuleFor(o => o.BetValue)
            .NotEmpty();
    }
}