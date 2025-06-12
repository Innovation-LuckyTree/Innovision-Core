using MediatR;

namespace Innovision.Core.Application.Requests.ApplicationVersions.Queries.GetAccountSelfLimit;

public record GetAccountSelfLimitQuery(long AccountId) : IRequest<SelfLimitDto>;
