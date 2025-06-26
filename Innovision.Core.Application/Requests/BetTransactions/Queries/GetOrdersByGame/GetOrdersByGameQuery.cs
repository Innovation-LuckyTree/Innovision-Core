using Innovision.Core.Application.Requests.Orders.Queries.GetOrders;
using MediatR;

namespace Innovision.Core.Application.Requests.Orders.Queries.GetOrdersByGame;

public record GetOrdersByGameQuery(int GameId) : IRequest<OrdersVm>;
