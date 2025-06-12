namespace Innovision.Core.Infrastructure.Games.Models.Responses
{
    public class PlayingListResponse
    {
        public int Size { get; set; }
        public int Offset { get; set; }
        public int Total { get; set; }
        public AccountListIds Data { get; set; }
    }

    public class AccountListIds
    {
        public List<long> AccountIds { get; set; }
    }
}
