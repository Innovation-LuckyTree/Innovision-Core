namespace Innovision.Core.Application.Requests.Games.Queries;

public record GameTypesVm(IEnumerable<GameTypesDto> GameTypes)
{
    public int Count
    {
        get
        {
            return GameTypes.Count();
        }
    }
}