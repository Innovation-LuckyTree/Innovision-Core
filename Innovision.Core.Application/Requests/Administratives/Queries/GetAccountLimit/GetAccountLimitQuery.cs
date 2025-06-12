using MediatR;

namespace Innovision.Core.Application.Requests.ApplicationVersions.Queries.GetAccountSelfLimit;

public record GetAccountLimitQuery(long AccountId) : IRequest<AccountLimitVm>;
