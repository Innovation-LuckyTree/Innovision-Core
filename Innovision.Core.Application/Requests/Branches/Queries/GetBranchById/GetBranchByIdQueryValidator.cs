using FluentValidation;

namespace Innovision.Core.Application.Requests.Branches.Queries.GetBranchById;

public class GetBranchByIdQueryValidator : AbstractValidator<GetBranchByIdQuery>
{
    public GetBranchByIdQueryValidator()
    {
        RuleFor(o => o.BranchId)
            .GreaterThan(0);
    }
}
