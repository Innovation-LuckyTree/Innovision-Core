namespace Innovision.Core.Application.Requests.Accounts.Users.AgentAndPlayer.Queries
{
    public class AgentListVm
    {
        public int Total { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }

        public List<AgentListDto> AgentList { get; set; }
    }
}
