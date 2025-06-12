namespace Innovision.Core.Application.Requests.Users.AgentAndPlayer.Queries.GetDownlinePlayers
{
    public class DownlinePlayersVm
    {
        public int Total { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }

        public List<DownlinePlayersDto> Results { get; set; }
    }
}
