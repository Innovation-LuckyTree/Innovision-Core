using Innovision.Core.Application.Common;
using MediatR;

namespace Innovision.Core.Application.Requests.Users.AgentAndPlayer.Queries.GetDownlineCounts;

public record GetDownlineCountsQuery(Guid AccountObjectId) : IRequest<ApiResponse<object>>;
