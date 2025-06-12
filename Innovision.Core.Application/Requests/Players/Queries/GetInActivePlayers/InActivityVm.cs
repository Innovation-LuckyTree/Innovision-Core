using Innovision.Core.Application.Requests.Users.Queries;

namespace Innovision.Core.Application.Requests.Players.Queries.GetInActivePlayers
{
    public class InActivityVm
    {
        public int Total { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string CurrentDrawTime { get; set; }

        public List<InActivityDto> Results { get; set; }
    }
}
