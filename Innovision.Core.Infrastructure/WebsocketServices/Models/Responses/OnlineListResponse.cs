namespace Innovision.Core.Infrastructure.WebsocketServices.Models.Responses
{
    public class OnlineListResponse
    {
        public int TotalCount { get; set; }
        public int TotalPages { get; set; }
        public int CurrentPage { get; set; }
        public List<long?> Data { get; set; }
    }
}
