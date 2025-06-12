namespace Innovision.Core.Infrastructure.Games.Models.Responses
{
    public class PlayingNowResponse
    {
        public int Total { get; set; }
        public int Offset { get; set; }
        public int Size { get; set; }
        public RowData Data { get; set; }
    }

    public class RowData
    {
        public string DrawTime { get; set; }
        public List<PlayingAccount> Accounts { get; set; }
    }

    public class PlayingAccount
    {
        public long AccountId { get; set; }
        public int BetCount { get; set; }
    }
}
