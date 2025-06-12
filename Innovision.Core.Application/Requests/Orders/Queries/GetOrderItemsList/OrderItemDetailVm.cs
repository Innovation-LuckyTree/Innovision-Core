namespace Innovision.Core.Application.Requests.Orders.Queries.GetOrderItemsList;

public record OrderItemDetailVm(IEnumerable<OrderItemDetailDto> OrderItems, long OffsetOrderItemId)
{
    public int Count
    {
        get
        {
            return OrderItems?.Count() ?? 0;
        }
    }
}
