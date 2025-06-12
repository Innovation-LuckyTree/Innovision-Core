using FluentValidation;

namespace Innovision.Core.Application.Requests.Accounts.Commands.UpdateUserPassword;

public class UpdateUserPasswordCommandValidator : AbstractValidator<UpdateUserPasswordCommand>
{
    public UpdateUserPasswordCommandValidator()
    {
        RuleFor(o => o.NewPassword)
            .Equal(e => e.ConfirmPassword)
            .NotEmpty();
    }
}
