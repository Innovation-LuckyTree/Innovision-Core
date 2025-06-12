namespace Innovision.Core.Application.Requests.JackpotWinners.Queries.GetJackpotDetailsByOrder;

public record JackpotDetailVm(IEnumerable<JackpotDetailDto> JackpotDetails)
{
    public int Count {
        get => JackpotDetails?.Count() ?? 0;
    }
}
