using Innovision.Core.Application.Requests.Orders.Queries.GetOrderDetail;

namespace Innovision.Core.Application.Requests.Orders.Queries.GetAccountOrdersRange;

public class GetAccountOrdersVm
{
    public int Total { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }

    public List<OrdersDto> Orders { get; set; }
}