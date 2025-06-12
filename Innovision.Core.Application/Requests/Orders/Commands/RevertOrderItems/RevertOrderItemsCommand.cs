using Innovision.Core.Application.Requests.Orders.Commands.ScheduleOrderItems;
using MediatR;

namespace Innovision.Core.Application.Requests.Orders.Commands.RevertOrderItems;

public record RevertOrderItemsCommand(ScheduleOrderItemsRequest ScheduleOrderItems) : IRequest<Unit>;
