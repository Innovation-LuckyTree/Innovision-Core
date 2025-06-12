using MediatR;

namespace Innovision.Core.Application.Requests.Players.Queries.GetPlayerMigrateAccount;

public record GetPlayerMigrateAccountQuery(Guid AccountObjectId) : IRequest<PlayerMigrateAccountDto>;
