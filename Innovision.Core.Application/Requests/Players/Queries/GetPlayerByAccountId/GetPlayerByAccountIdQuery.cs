using MediatR;

namespace Innovision.Core.Application.Requests.Players.Queries.GetPlayerByAccountId;

public record GetPlayerByAccountIdQuery(long AccountId) : IRequest<PlayerAccountDto>;
