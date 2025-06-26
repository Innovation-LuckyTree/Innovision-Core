using MediatR;

namespace Innovision.Core.Application.Requests.Orders.Queries.GetBetTransactions;

public record GetBetTransactionsQuery(IEnumerable<long> BetTransactionIds) : IRequest<BetTransactionVm>;
