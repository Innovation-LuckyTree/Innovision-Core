using MediatR;

namespace Innovision.Core.Application.Requests.Orders.Queries.GetBetTransactionDetail;

public record GetOrderGrossQuery(DateTime DateFrom, DateTime DateTo) : IRequest<OrderGrossVm>;
