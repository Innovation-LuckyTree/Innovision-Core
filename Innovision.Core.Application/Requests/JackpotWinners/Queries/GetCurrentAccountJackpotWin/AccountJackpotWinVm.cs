namespace Innovision.Core.Application.Requests.JackpotWinners.Queries.GetCurrentAccountJackpotWin;

public record AccountJackpotWinVm(IEnumerable<AccountJackpotWinnerDto> JackpotWins)
{
    public int TotalCount { get; set; }
    public int Count
    {
        get => JackpotWins?.Count() ?? 0;
    }

    public int PendingCount
    {
        get => JackpotWins?.Where(o => o.JackpotWinnerStatusId == 1)?.Count() ?? 0;
    }

    public int CompletedCount
    {
        get => JackpotWins?.Where(o => o.JackpotWinnerStatusId == 4)?.Count() ?? 0;
    }
}
