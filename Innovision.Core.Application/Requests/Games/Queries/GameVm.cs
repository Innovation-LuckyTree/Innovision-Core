namespace Innovision.Core.Application.Requests.Games.Queries;

public record GameVm(IEnumerable<GameDto> Games)
{
    public int Count
    {
        get
        {
            return Games.Count();
        }
    }
}