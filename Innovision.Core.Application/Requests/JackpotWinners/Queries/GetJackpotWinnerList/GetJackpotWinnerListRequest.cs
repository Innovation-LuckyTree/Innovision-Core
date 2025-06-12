using Innovision.Core.Common.Models;

namespace Innovision.Core.Application.Requests.JackpotWinners.Queries.GetJackpotWinnerList;

public class GetJackpotWinnerListRequest
{
    public long? JackpotStatusId { get; set; }
    public PagedQuery? PageQuery { get; set; }
}

