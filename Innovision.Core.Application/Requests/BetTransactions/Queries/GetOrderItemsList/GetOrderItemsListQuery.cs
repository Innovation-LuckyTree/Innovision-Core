using MediatR;

namespace Innovision.Core.Application.Requests.Orders.Queries.GetBetTransactionsList;

public record GetBetTransactionsListQuery(long BetTransactionId, int Size) : IRequest<BetTransactionDetailVm>;
