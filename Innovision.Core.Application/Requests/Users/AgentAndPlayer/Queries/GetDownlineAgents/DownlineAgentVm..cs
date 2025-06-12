using Innovision.Core.Application.Requests.Accounts.Users.MasterAgent;

namespace Innovision.Core.Application.Requests.Users.AgentAndPlayer.Queries.GetDownlineAgents
{
    public class DownlineAgentVm
    {
        public int Total { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }

        public List<DownlineAgentsDto> Results { get; set; }
    }
}
