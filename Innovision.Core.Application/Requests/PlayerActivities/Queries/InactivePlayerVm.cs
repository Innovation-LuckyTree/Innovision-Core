namespace Innovision.Core.Application.Requests.PlayerActivities.Queries
{
    public class InactivePlayerVm
    {
        public int Total { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }

        public List<InactivePlayerDto> Results { get; set; }
    }
}
