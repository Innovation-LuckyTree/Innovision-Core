using Innovision.Core.Application.Common;
using MediatR;

namespace Innovision.Core.Application.Requests.PlayerActivities.Queries.GetPlayerActivityByAccountId
{
    public record GetPlayerActivityByAccountIdQuery(long AccountId) : IRequest<ApiResponse<PlayerActivityDto>>;
}
