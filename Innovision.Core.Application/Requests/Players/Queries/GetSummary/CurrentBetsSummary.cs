namespace Innovision.Core.Application.Requests.Players.Queries.GetSummary;

public class CurrentBetsSummary
{
    public int ActivePlayer { get; set; } = 0;
    public int OnlinePlayer { get; set; } = 0;
    public int OnlinePlayerWithoutBet { get; set; } = 0;
    public int TotalPlayer { get; set; }
    public decimal TotalBetAmount { get; set; }
    public int InTransactions { get; set; }
    public int OutTransactions { get; set; }
    public int OfflinePlayer
    {
        get => TotalPlayer - OnlinePlayer;
    }
}
