namespace Innovision.Core.Application.Requests.Accounts.Users.AgentAndPlayer.Queries
{
    public class PlayerListVm
    {
        public int Total { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }

        public List<PlayerListDto> PlayerList { get; set; }
    }
}
