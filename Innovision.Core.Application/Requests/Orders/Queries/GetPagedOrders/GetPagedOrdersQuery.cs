using MediatR;

namespace Innovision.Core.Application.Requests.Orders.Queries.GetPagedOrders;

public record GetPagedOrdersQuery(int Start, int Size) : IRequest<OrdersVm>
{
    public long? StartOrderId { get; set; } = null;
    public  DateTimeOffset? StartDate { get; set; } = null;
    public  DateTimeOffset? EndDate { get; set; } = null;

}
