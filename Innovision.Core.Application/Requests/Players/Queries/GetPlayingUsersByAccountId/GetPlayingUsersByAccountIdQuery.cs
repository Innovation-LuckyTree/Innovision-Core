using Innovision.Core.Application.Common;
using Innovision.Core.Application.Requests.Users.Queries;
using MediatR;

namespace Innovision.Core.Application.Requests.Players.Queries.GetPlayingUsersByAccountId;

public record GetPlayingUsersByAccountIdQuery(IEnumerable<long> AccountIds) : IRequest<ApiResponse<IEnumerable<UserStatusDto>>>;
