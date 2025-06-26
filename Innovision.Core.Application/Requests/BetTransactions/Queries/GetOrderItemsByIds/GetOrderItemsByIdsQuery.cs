using MediatR;

namespace Innovision.Core.Application.Requests.Orders.Queries.GetBetTransactionsByIds;

public record GetBetTransactionsByIdsQuery(IEnumerable<long> BetTransactionIds) : IRequest<BetTransactionDetailsVm>;
