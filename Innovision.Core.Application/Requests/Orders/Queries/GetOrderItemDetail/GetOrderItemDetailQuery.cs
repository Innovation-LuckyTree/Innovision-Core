using Innovision.Core.Application.Requests.Orders.Queries.GetOrderItemsList;
using MediatR;

namespace Innovision.Core.Application.Requests.Orders.Queries.GetOrderItemDetail;

public record GetOrderItemDetailQuery(long OrderItemId) : IRequest<OrderItemDetailDto>;
