using Innovision.Core.Application.Common;
using MediatR;

namespace Innovision.Core.Application.Requests.Roles.Queries.GetRoles;

public record GetRolesQuery(int? CompanyId) : IRequest<ApiResponse<List<UserTypeDto>>> { }
