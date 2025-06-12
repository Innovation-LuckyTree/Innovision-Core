using MediatR;

namespace Innovision.Core.Application.Requests.Orders.Queries.GetOrderItems;

public record GetOrderItemsQuery(IEnumerable<long> OrderItemIds) : IRequest<OrderItemVm>;
