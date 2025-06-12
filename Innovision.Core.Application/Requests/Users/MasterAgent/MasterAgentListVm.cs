using Innovision.Core.Application.Requests.Accounts.Users.Operator;

namespace Innovision.Core.Application.Requests.Accounts.Users.MasterAgent
{
    public class MasterAgentListVm
    {
        public int Total { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }

        public List<MasterAgentListDto> MasterAgentList { get; set; }
    }
}
