using MediatR;

namespace Innovision.Core.Application.Requests.Orders.Commands.ScheduleBetTransactions;

public record ScheduleBetTransactionsCommand(IEnumerable<ScheduleBetTransactionsRequest> ScheduleBetTransactions) : IRequest<Unit>;
