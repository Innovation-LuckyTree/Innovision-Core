using Innovision.Core.Application.Common;
using Innovision.Core.Common.Models;
using MediatR;

namespace Innovision.Core.Application.Requests.Users.Queries.GetFullyVerifiedUsers;

public record GetFullyVerifiedUsersQuery(int? CompanyId, PagedQuery? PagedQuery, DateTime? DateFrom, DateTime? DateTo) : IRequest<ApiResponse<UserStatusVm>>;
