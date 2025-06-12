using Innovision.Core.Application.Common;
using Innovision.Core.Common.Models;
using MediatR;

namespace Innovision.Core.Application.Requests.Users.Queries.GetUserForVerification;

public class GetUserForVerificationQuery : IRequest<ApiResponse<UsersVerificationVm>>
{
    public Guid? CompanyId { get; set; }
    public int? BranchId { get; set; }
    public  DateTimeOffset? DateFrom { get; set; }
    public  DateTimeOffset? DateTo { get; set; }
    public PagedQuery? PagedQuery { get; set; }
}
