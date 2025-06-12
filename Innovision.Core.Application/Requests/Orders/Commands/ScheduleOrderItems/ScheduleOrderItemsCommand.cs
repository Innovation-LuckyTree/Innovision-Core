using MediatR;

namespace Innovision.Core.Application.Requests.Orders.Commands.ScheduleOrderItems;

public record ScheduleOrderItemsCommand(IEnumerable<ScheduleOrderItemsRequest> ScheduleOrderItems) : IRequest<Unit>;
