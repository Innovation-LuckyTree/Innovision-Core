namespace Innovision.Core.Application.Requests.Orders.Queries.GetPagedOrders;

public record OrdersVm(IEnumerable<OrdersDto> Orders, int TotalCount)
{
    public int Count
    {
        get => Orders?.Count() ?? 0;
    }
}