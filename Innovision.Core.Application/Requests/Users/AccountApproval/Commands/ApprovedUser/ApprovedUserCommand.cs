using Innovision.Core.Application.Common;
using MediatR;

namespace Innovision.Core.Application.Requests.Accounts.Users.AccountApproval.Commands;

public class ApprovedUserCommand : IRequest<ApiResponse<bool>>
{
    public Guid AccountInfoId { get; set; }
    public int? UserTypeId { get; set; }
    public decimal? Commission { get; set; }
}
