using MediatR;

namespace Innovision.Core.Application.Requests.Orders.Queries.GetOrderItemsList;

public record GetOrderItemsListQuery(long OrderItemId, int Size) : IRequest<OrderItemDetailVm>;
