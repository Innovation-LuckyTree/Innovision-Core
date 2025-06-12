using MediatR;

namespace Innovision.Core.Application.Requests.Orders.Queries.GetOrderItemsByIds;

public record GetOrderItemsByIdsQuery(IEnumerable<long> OrderItemIds) : IRequest<OrderItemDetailsVm>;
