using Innovision.Core.Application.Requests.Orders.Queries.GetOrderItemsList;

namespace Innovision.Core.Application.Requests.Orders.Queries.GetOrderItemsByIds;

public record OrderItemDetailsVm(IEnumerable<OrderItemDetailDto> OrderItems)
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
