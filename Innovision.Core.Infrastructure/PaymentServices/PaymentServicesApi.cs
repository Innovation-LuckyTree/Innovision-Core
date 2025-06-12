using Innovision.Core.Infrastructure.Interfaces;
using Innovision.Core.Infrastructure.Helpers;
using Innovision.Core.Infrastructure.PaymentServices.Models.Requests;
using System.Net.Http.Json;
using Innovision.Core.Infrastructure.PaymentServices.Models;

namespace Innovision.Core.Infrastructure.PaymentServices;

public class PaymentServicesApi : AbstractApiClient, IPaymentServicesApi
{
    private readonly string _clientId;

    public PaymentServicesApi(HttpClient? client, IAppConfig appConfig) : base(nameof(PaymentServicesApi), client)
    {
        _client.BaseAddress = new Uri(appConfig.PaymentApiClient.BaseAddressUrl);
        _client.DefaultRequestHeaders.Add("Resource", appConfig.PaymentApiClient.Resource);

        _clientId = appConfig.AppId;
    }

    public async Task<object> GetAccountTransactionRequest(GetAccountTransactionRequest request, CancellationToken cancellationToken)
    {
        var response = await _client.PostAsJsonAsync($"/api/accountWallet/transaction/search", request, cancellationToken);
        if (!response.IsSuccessStatusCode)
            throw new Exception("Connecting to core api was unsuccessful.");

        var content = await response.Content.ReadFromJsonAsync<object>();
        return content!;
    }

    public async Task<List<AccountBalance>> GetAccountBalancesRequest(GetAccountBalanceRequest request, CancellationToken cancellationToken)
    {
        var response = await _client.PostAsJsonAsync($"/api/accountWallet/balances", request, cancellationToken);
        if (!response.IsSuccessStatusCode)
            throw new Exception("Connecting to core api was unsuccessful.");

        var content = await response.Content.ReadFromJsonAsync<List<AccountBalance>>();
        return content!;
    }

    public async Task<CreditTransactionResponse> GetCreditTransaction(long CreditTransId, CancellationToken cancellationToken)
    {
        var response = await _client.GetAsync($"/api/credit/transaction?CreditTransId={CreditTransId}", cancellationToken);
        if (!response.IsSuccessStatusCode)
            throw new Exception("Connecting to core api was unsuccessful.");

        var content = await response.Content.ReadFromJsonAsync<CreditTransactionResponse>();
        return content!;
    }
}
