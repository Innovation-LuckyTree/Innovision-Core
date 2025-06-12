using Innovision.Core.Infrastructure.PaymentServices.Models;
using Innovision.Core.Infrastructure.PaymentServices.Models.Requests;

namespace Innovision.Core.Infrastructure.Interfaces;
public interface IPaymentServicesApi
{
    Task<object> GetAccountTransactionRequest(GetAccountTransactionRequest request, CancellationToken cancellationToken);
    Task<List<AccountBalance>> GetAccountBalancesRequest(GetAccountBalanceRequest request, CancellationToken cancellationToken);
    Task<CreditTransactionResponse> GetCreditTransaction(long CreditTransId, CancellationToken cancellationToken);
}
