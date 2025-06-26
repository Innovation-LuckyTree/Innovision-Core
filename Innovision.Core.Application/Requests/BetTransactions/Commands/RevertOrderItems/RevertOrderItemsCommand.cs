using Innovision.Core.Application.Requests.Orders.Commands.ScheduleBetTransactions;
using MediatR;

namespace Innovision.Core.Application.Requests.Orders.Commands.RevertBetTransactions;

public record RevertBetTransactionsCommand(ScheduleBetTransactionsRequest ScheduleBetTransactions) : IRequest<Unit>;
