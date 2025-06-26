using MediatR;

namespace Innovision.Core.Application.Requests.Orders.Queries.GetPagedOrders;

public record GetPagedOrdersQuery(int Start, int Size) : IRequest<OrdersVm>
{
    public long? StartOrderId { get; set; } = null;
    public DateTime? StartDate { get; set; } = null;
    public DateTime? EndDate { get; set; } = null;

}
