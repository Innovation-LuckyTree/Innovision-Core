using Innovision.Core.Infrastructure.Interfaces;
using Innovision.Core.Infrastructure.Helpers;
using Innovision.Core.Infrastructure.AccountServices.Models.Responses;
using System.Net.Http.Json;
using Innovision.Core.Infrastructure.AccountServices.Models.Requests;
using Microsoft.Extensions.Logging;

namespace Innovision.Core.Infrastructure.AccountServices;

public class AccountServiceApi : AbstractApiClient, IAccountServiceApi
{
    private readonly IAppConfig _appConfig;
    private readonly ILogger<AccountServiceApi> _logger;

    public AccountServiceApi(HttpClient? client, IAppConfig appConfig, ILogger<AccountServiceApi> logger) : base(nameof(AccountServiceApi), client)
    {
        _client.BaseAddress = new Uri(appConfig.AccountServiceApiClient.BaseAddressUrl);
        _client.DefaultRequestHeaders.Add("Resource", appConfig.AccountServiceApiClient.Resource);

        _appConfig = appConfig;
        _logger = logger;
    }

    public async Task<T> GetAccountWalletTransaction<T>(CancellationToken cancellationToken) where T : class
    {
        var response = await _client.GetAsync($"/api/accountTransaction/transactions", cancellationToken);
        if (!response.IsSuccessStatusCode)
        {
            return null;
        }

        var content = await response.Content.ReadFromJsonAsync<T>();
        return content!;
    }

    public async Task<AccountBalanceResponse> GetAccountWalletBalance(CancellationToken cancellationToken)
    {
        var response = await _client.GetAsync($"/api/accountTransaction/credits", cancellationToken);
        if (!response.IsSuccessStatusCode)
        {
            return null;
        }

        var content = await response.Content.ReadFromJsonAsync<AccountBalanceResponse>();
        return content!;
    }

    public async Task<AccountBalanceResponse> GetAccountWalletBalanceById(Guid accountObjectId, CancellationToken cancellationToken)
    {
        var response = await _client.GetAsync($"/api/accountTransaction/credits/{accountObjectId}", cancellationToken);
        if (!response.IsSuccessStatusCode)
        {
            return null;
        }

        var content = await response.Content.ReadFromJsonAsync<AccountBalanceResponse>();
        return content!;
    }

    public async Task AccountCashIn(AddDebitTransactionRequest request, CancellationToken cancellationToken)
    {
        var response = await _client.PostAsJsonAsync("/api/accountTransaction/cash-in", request, cancellationToken);

        response.EnsureSuccessStatusCode();
    }

    public async Task AddBet(AddCreditTransactionRequest request, CancellationToken cancellationToken)
    {
        var response = await _client.PostAsJsonAsync("/api/accountTransaction/bet", request, cancellationToken);

        response.EnsureSuccessStatusCode();
    }

    public async Task<object> AccountWithdraw(AddCreditTransactionRequest request, CancellationToken cancellationToken)
    {
        var response = await _client.PostAsJsonAsync("/api/accountTransaction/withdraw", request, cancellationToken);
        var content = await response.Content.ReadFromJsonAsync<object>();
        return content!;
    }

    public async Task<object> WithdrawBalance(WithdrawBalanceRequest request, CancellationToken cancellationToken)
    {
        var response = await _client.PostAsJsonAsync("/api/accountTransaction/balance/withdraw", request, cancellationToken);
        var content = await response.Content.ReadFromJsonAsync<object>();
        return content!;
    }

    public async Task<object> AddWalletAccount(AccountTransactionRequest request, CancellationToken cancellationToken)
    {
        var response = await _client.PostAsJsonAsync($"api/AccountTransaction/account/wallet", request, cancellationToken);
        var content = await response.Content.ReadFromJsonAsync<object>(cancellationToken);
        return content!;
    }

    public async Task<object> AddBonusAccount(BonusWalletRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var response = await _client.PostAsJsonAsync($"api/bonus-account", request, cancellationToken);
            var content = await response.Content.ReadFromJsonAsync<object>(cancellationToken);
            return content!;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<object> CreditBonusAccount(CreditBonusRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var response = await _client.PostAsJsonAsync($"api/bonus-account/credit", request, cancellationToken);
            var content = await response.Content.ReadFromJsonAsync<object>(cancellationToken);
            return content!;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<BonusAccountTransactionResponse> GetBonusAccountTransaction(BonusAccountTransactionRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var response = await _client.PostAsJsonAsync($"api/bonus-account/transactions/promotion", request, cancellationToken);
            var content = await response.Content.ReadFromJsonAsync<BonusAccountTransactionResponse>(cancellationToken);
            return content!;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<BonusAccountByPromotionResponse> ProcessReturnBonus(ProcessReturnBonusRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var response = await _client.PostAsJsonAsync($"api/bonus-account/process/return", request, cancellationToken);
            var content = await response.Content.ReadFromJsonAsync<BonusAccountByPromotionResponse>(cancellationToken);
            return content!;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }    

    #region payment provider
    public async Task<AccountResponse> CreatePaymentAccount(CreateAccountRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var response = await _client.PostAsJsonAsync("/api/provider/account", request, cancellationToken);

            if (!response.IsSuccessStatusCode)
            {
                _logger.LogError($"Failed to create payment account! Error Code {response.StatusCode}");
                return null;
            }

            var jsonString = await response.Content.ReadAsStringAsync();

            _logger.LogInformation($"Payment Account Created!: {jsonString}");

            // if (string.IsNullOrEmpty(jsonString))
            // {
            //     return null;
            // }

            var content = await response.Content.ReadFromJsonAsync<AccountResponse>(cancellationToken);
            return content!;
        }
        catch (Exception ex)
        {
            return null;
        }
    }
    #endregion
}