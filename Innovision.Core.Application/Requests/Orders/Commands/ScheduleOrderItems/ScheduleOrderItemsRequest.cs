namespace Innovision.Core.Application.Requests.Orders.Commands.ScheduleOrderItems;

public class ScheduleOrderItemsRequest
{
    public long GameScheduleId { get; set; }
    public IEnumerable<long> OrderItems { get; set; }
}

