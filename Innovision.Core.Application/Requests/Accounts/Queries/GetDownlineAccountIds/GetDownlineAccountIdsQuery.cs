using MediatR;

namespace Innovision.Core.Application.Requests.Accounts.Queries.GetDownlineAccountIds;

public record GetDownlineAccountIdsQuery(long AccountId) : IRequest<DownlineAccountIdDto>;
