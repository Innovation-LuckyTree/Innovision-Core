using Innovision.Core.Infrastructure.AccountServices.Models.Requests;
using Innovision.Core.Infrastructure.AccountServices.Models.Responses;

namespace Innovision.Core.Infrastructure.Interfaces;

public interface IAccountServiceApi
{
    Task<T> GetAccountWalletTransaction<T>(CancellationToken cancellationToken) where T : class;
    Task<AccountBalanceResponse> GetAccountWalletBalance(CancellationToken cancellationToken);
    Task<AccountBalanceResponse> GetAccountWalletBalanceById(Guid accountObjectId, CancellationToken cancellationToken);
    Task AccountCashIn(AddDebitTransactionRequest request, CancellationToken cancellationToken);
    Task AddBet(AddCreditTransactionRequest request, CancellationToken cancellationToken);
    Task<object> AccountWithdraw(AddCreditTransactionRequest request, CancellationToken cancellationToken);
    Task<AccountResponse> CreatePaymentAccount(CreateAccountRequest request, CancellationToken cancellationToken);
    Task<object> WithdrawBalance(WithdrawBalanceRequest request, CancellationToken cancellationToken);
    Task<object> AddWalletAccount(AccountTransactionRequest request, CancellationToken cancellationToken);
    Task<BonusAccountTransactionResponse> GetBonusAccountTransaction(BonusAccountTransactionRequest request, CancellationToken cancellationToken);
    Task<BonusAccountByPromotionResponse> ProcessReturnBonus(ProcessReturnBonusRequest request, CancellationToken cancellationToken);
    Task<object> AddBonusAccount(BonusWalletRequest request, CancellationToken cancellationToken);
    Task<object> CreditBonusAccount(CreditBonusRequest request, CancellationToken cancellationToken);
}
