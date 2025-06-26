using MediatR;

namespace Innovision.Core.Application.Requests.Orders.Queries.GetUserUnusedItems;

public record GetUserUnusedItemsQuery(int GameId) : IRequest<BetTransactionVm>;
