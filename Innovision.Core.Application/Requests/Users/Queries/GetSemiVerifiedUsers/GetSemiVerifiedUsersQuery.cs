using Innovision.Core.Application.Common;
using Innovision.Core.Common.Models;
using MediatR;

namespace Innovision.Core.Application.Requests.Users.Queries.GetSemiVerifiedUsers;

public record GetSemiVerifiedUsersQuery(int? CompanyId, PagedQuery? PagedQuery, DateTime? DateFrom, DateTime? DateTo) : IRequest<ApiResponse<UserStatusVm>>;
