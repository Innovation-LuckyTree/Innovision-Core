using Innovision.Core.Application.Common;
using MediatR;

namespace Innovision.Core.Application.Requests.Users.Queries.GetSystemUsers;

public record GetSystemUsersQuery(int? CompanyId, int? BranchId, int? RoleId, bool? IsDownline) : IRequest<ApiResponse<List<SystemUserDto>>>;
