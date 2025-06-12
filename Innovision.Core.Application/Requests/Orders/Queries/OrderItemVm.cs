namespace Innovision.Core.Application.Requests.Orders.Queries;

public record OrderItemVm(IEnumerable<OrderItemDto> OrderItems)
{
    public decimal TotalAmount
    {
        get
        {
            if ((OrderItems?.Count() ?? 0) > 0)
                return OrderItems.Sum(o => o.AmountBet);

            return 0;
        }
    }

    public int TotalCount
    {
        get
        {
            return OrderItems?.Count() ?? 0;
        }
    }
}