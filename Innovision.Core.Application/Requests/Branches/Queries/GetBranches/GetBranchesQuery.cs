using Innovision.Core.Application.Common;
using Innovision.Core.Common.Models;
using MediatR;

namespace Innovision.Core.Application.Requests.Branches.Queries.GetBranches;

public class GetBranchesQuery : IRequest<ApiResponse<BranchListVm>>
{
    public Guid? CompanyId { get; set; }
    public PagedQuery? PagedQuery { get; set; }
}
