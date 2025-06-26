using Innovision.Core.Application.Requests.Orders.Queries.GetBetTransactionsList;
using MediatR;

namespace Innovision.Core.Application.Requests.Orders.Queries.GetBetTransactionDetail;

public record GetBetTransactionDetailQuery(long BetTransactionId) : IRequest<BetTransactionDetailDto>;
