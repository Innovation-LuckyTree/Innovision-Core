using MediatR;

namespace Innovision.Core.Application.Requests.Orders.Queries.GetOrderedByIdItems;

public record GetOrderedByIdItemsQuery(long OrderId) : IRequest<OrderItemVm>;
