namespace Innovision.Core.Application.Requests.Orders.Commands.AdvanceScheduleOrderItem;

public class AdvanceScheduleOrderItemsRequest
{
    public int GameType { get; set; }
    public IEnumerable<long> OrderItems { get; set; }
}
