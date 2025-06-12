using Innovision.Core.Application.Requests.Accounts.Queries.GetApprovedAccounts;
using MediatR;

namespace Innovision.Core.Application.Requests.Branches.Queries.GetAccountsByBranchId;

public class GetAccountsByBranchIdQuery : IRequest<AccountVm>
{
  public int BranchId { get; set; }
  public List<int>? UserTypeIds { get; set; }
}
