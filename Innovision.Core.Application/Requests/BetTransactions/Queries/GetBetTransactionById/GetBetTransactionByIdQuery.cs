using MediatR;

namespace Innovision.Core.Application.Requests.BetTransactions.Queries.GetBetTransactionById;

public record GetBetTransactionByIdQuery(long BetTransactionId) : IRequest<BetTransactionDto>;
