using MediatR;

namespace Innovision.Core.Application.Requests.Withdrawals.Queries.GetWithrawalDetailById;

public record GetWithrawalDetailByIdQuery(long TransactionId) : IRequest<WithdrawalDto>;
