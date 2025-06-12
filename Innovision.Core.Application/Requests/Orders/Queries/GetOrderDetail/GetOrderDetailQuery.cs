using MediatR;

namespace Innovision.Core.Application.Requests.Orders.Queries.GetOrderDetail;

public record GetOrderDetailQuery(long OrderId) : IRequest<OrdersDto>;
