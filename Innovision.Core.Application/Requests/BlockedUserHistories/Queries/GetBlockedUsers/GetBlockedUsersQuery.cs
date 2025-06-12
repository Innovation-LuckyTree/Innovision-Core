using Innovision.Core.Common.Models;
using MediatR;

namespace Innovision.Core.Application.Requests.BlockedUserHistories.Queries.GetBlockedUsers;

public class GetBlockedUsersListQuery : IRequest<BlockUsersVm>
{
  public PagedQuery? PagedQuery { get; set; }
}
