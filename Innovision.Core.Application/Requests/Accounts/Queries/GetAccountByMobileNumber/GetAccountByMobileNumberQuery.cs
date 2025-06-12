using MediatR;

namespace Innovision.Core.Application.Requests.Accounts.Queries.GetAccountByMobileNumber;

public record GetAccountByMobileNumberQuery(string MobileNumber) : IRequest<AccountDto>;
