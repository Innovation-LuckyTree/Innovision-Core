using MediatR;

namespace Innovision.Core.Application.Requests.Orders.Commands.UseOrderedItem;

public record UseOrderedItemCommand(long GameScheduleId, IEnumerable<long> OrderItems) : IRequest<Unit>;
