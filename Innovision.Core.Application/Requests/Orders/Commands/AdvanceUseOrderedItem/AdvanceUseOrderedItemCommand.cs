using MediatR;

namespace Innovision.Core.Application.Requests.Orders.Commands.AdvanceUseOrderedItem;

public record AdvanceUseOrderedItemCommand(DateTime Date, TimeSpan DrawTime, IEnumerable<long> OrderItems) : IRequest<Unit>;
