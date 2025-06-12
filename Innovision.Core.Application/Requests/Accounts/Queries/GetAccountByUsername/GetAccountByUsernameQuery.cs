using MediatR;

namespace Innovision.Core.Application.Requests.Accounts.Queries.GetAccountByUsername;

public record GetAccountByUsernameQuery(string Username) : IRequest<AccountDto>
{
    public bool IsPlayer { get; set; } = false;
}
