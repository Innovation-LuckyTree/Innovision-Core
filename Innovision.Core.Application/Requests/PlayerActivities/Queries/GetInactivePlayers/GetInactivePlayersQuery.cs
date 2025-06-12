using Innovision.Core.Application.Common;
using Innovision.Core.Common.Models;
using MediatR;

namespace Innovision.Core.Application.Requests.PlayerActivities.Queries.GetInactivePlayers;

public record GetInactivePlayersQuery(PagedQuery? PagedQuery) : IRequest<ApiResponse<InactivePlayerVm>>;
