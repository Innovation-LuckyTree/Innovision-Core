using Innovision.Core.Application.Common;
using Innovision.Core.Application.Requests.Users.Queries;
using Innovision.Core.Common.Models;
using MediatR;

namespace Innovision.Core.Application.Requests.Players.Queries.GetLockedUsers;

public record GetLockedUsersQuery(Guid CompanyObjectId, PagedQuery? PagedQuery) : IRequest<ApiResponse<UserStatusVm>>;