using Innovision.Core.Application.Requests.BlockedUserHistories.Queries.GetBlockedUsers;
using MediatR;

namespace Innovision.Core.Application.Requests.BlockedUserHistories.Queries.GetUsersBlockHistories;

public record GetUsersBlockHistoriesQuery(long AccountInfoId) : IRequest<BlockUsersVm>;
