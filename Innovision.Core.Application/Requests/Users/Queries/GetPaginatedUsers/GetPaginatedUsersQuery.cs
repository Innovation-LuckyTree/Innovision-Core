using Innovision.Core.Application.Common;
using Innovision.Core.Common.Models;
using MediatR;

namespace Innovision.Core.Application.Requests.Users.Queries.GetPaginatedUsers;

public class GetPaginatedUsersQuery : IRequest<ApiResponse<UserListVm>>
{
    public Guid? CompanyId { get; set; }
    public int? BranchId { get; set; }
    public int? UserType { get; set; }
    public PagedQuery? PagedQuery { get; set; }
}

