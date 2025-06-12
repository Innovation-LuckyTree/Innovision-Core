using FluentValidation;

namespace Innovision.Core.Application.Requests.Deposits.Queries.LookupReference;

public class LookupReferenceQueryValidator : AbstractValidator<LookupReferenceQuery>
{
    public LookupReferenceQueryValidator()
    {
        RuleFor(o => o.TransactionNo)
            .NotEmpty();
    }
}

