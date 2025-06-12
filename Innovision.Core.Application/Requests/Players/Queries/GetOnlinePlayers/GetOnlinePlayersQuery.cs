using Innovision.Core.Application.Common;
using Innovision.Core.Application.Requests.Users.Queries;
using Innovision.Core.Common.Models;
using MediatR;

namespace Innovision.Core.Application.Requests.Players.Queries.GetOnlinePlayers;

public record GetOnlinePlayersQuery(Guid CompanyObjId, PagedQuery? PagedQuery) : IRequest<ApiResponse<UserStatusVm>>;
