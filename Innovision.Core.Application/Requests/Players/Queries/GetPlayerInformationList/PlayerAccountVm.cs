namespace Innovision.Core.Application.Requests.Players.Queries.GetPlayerInformationList;

public record PlayerAccountVm(IEnumerable<PlayerAccountDto> Accounts)
{
    public int Count
    {
        get => Accounts?.Count() ?? 0;
    }
}