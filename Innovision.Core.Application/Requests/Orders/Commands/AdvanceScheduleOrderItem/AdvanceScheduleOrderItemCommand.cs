using MediatR;

namespace Innovision.Core.Application.Requests.Orders.Commands.AdvanceScheduleOrderItem;

public record AdvanceScheduleOrderItemCommand(DateTime Date, string GameTime, IEnumerable<AdvanceScheduleOrderItemsRequest> ScheduleOrderItems) : IRequest<Unit>;
