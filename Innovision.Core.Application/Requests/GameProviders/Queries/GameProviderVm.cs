namespace Innovision.Core.Application.Requests.GameProviders;


public record GameProviderVm(IEnumerable<GameProvidersDto> GameProviders)
{
    public int Count
    {
        get
        {
            return GameProviders.Count();
        }
    }
}