using Innovision.Core.Application.Requests.Accounts.Queries.GetCurrentAccountInfo;
using MediatR;

namespace Innovision.Core.Application.Requests.Accounts.Queries.GetAccountByPaymentAccount;

public record GetAccountByPaymentAccountQuery(string PaymentAccountId) : IRequest<AccountInfoDto>;
