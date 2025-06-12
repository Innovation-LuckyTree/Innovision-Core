using Innovision.Core.Application.Common;
using Innovision.Core.Application.Requests.Users.Queries;
using Innovision.Core.Common.Models;
using MediatR;

namespace Innovision.Core.Application.Requests.Players.Queries.GetOfflinePlayers;
public record GetOfflinePlayersQuery(Guid CompanyObjId, PagedQuery? PagedQuery) : IRequest<ApiResponse<UserStatusVm>>;
