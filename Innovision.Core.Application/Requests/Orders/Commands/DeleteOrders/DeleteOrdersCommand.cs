using MediatR;

namespace Innovision.Core.Application.Requests.Orders.Commands.AddItemOrder;

public record DeleteOrdersCommand(long OrderId) : IRequest<Unit>;
