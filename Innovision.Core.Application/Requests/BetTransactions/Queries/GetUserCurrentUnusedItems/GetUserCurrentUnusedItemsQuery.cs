using MediatR;

namespace Innovision.Core.Application.Requests.Orders.Queries.GetUserCurrentUnusedItems;

public record GetUserCurrentUnusedItemsQuery(int GameId, DateTime OpenSchedule) : IRequest<BetTransactionVm>;
