using MediatR;

namespace Innovision.Core.Application.Requests.Orders.Queries.GetOrderItemDetail;

public record GetOrderGrossQuery(DateTime DateFrom, DateTime DateTo) : IRequest<OrderGrossVm>;
