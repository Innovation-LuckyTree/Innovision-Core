namespace Innovision.Core.Application.Requests.JackpotWinners.Queries.GetJackpotWinnersByGame;

public record JackpotWinnerInfoVm(IEnumerable<JackpotWinnerInfo> JackpotWinners)
{
    public int Count
    {
        get
        {
            return JackpotWinners?.Count() ?? 0;
        }
    }
}
