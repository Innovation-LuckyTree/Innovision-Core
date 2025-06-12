namespace Innovision.Core.Application.Requests.Accounts.Queries.GetUnverifiedUsersFor7Days;

public record UnverifiedAccountVm(IEnumerable<UnverifiedAccountDto> Accounts)
{
    public int Count {
        get => Accounts?.Count() ?? 0;
    }
}