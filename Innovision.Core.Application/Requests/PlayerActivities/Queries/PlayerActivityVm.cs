namespace Innovision.Core.Application.Requests.PlayerActivities.Queries
{
    public class PlayerActivityVm
    {
        public int Total { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }

        public List<PlayerActivityDto> Results { get; set; }
    }
}
