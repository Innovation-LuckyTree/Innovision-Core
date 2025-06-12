using Innovision.Core.Application.Common;
using Innovision.Core.Application.Requests.Users.AgentAndPlayer.Queries.GetDownlinePlayers;
using Innovision.Core.Common.Models;
using MediatR;

namespace Innovision.Core.Application.Requests.Accounts.Users.AgentAndPlayer.Queries.GetPlayers;

public class GetPlayersQuery : IRequest<ApiResponse<DownlinePlayersVm>>
{
    public Guid? CompanyId { get; set; }
    public int? BranchId { get; set; }
    public  DateTimeOffset? DateFrom { get; set; }
    public  DateTimeOffset? DateTo { get; set; }
    public bool? downloadReport { get; set; }
    public PagedQuery? PagedQuery { get; set; }
}
