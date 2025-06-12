namespace Innovision.Core.Application.Requests.Players.Queries;

public record PlayerAccountVm(IEnumerable<PlayerAccountDto> Players)
{
    public int Count
    {
        get
        {
            return Players.Count();
        }
    }
}