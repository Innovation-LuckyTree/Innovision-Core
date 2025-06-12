using System.Net.Http.Json;
using Innovision.Core.Infrastructure.Games.Models.Responses;
using Innovision.Core.Infrastructure.Helpers;
using Innovision.Core.Infrastructure.Interfaces;
using Innovision.Core.Infrastructure.Support.Models.Response;

class SupportApi : AbstractApiClient, ISupportApi
{
    private readonly string _clientId;

    public SupportApi(HttpClient? client, IAppConfig appConfig) : base(nameof(SupportApi), client)
    {
        _client.BaseAddress = new Uri(appConfig.SupportApiClient.BaseAddressUrl);
        _client.DefaultRequestHeaders.Add("Resource", appConfig.SupportApiClient.Resource);

        _clientId = appConfig.AppId;
    }

    public async Task<GetCasesResponse> GetCases(GetCasesRequest request, CancellationToken cancellationToken)
    {
        var response = await _client.PostAsJsonAsync($"/api/case/search", request, cancellationToken);

        var content = await response.Content.ReadFromJsonAsync<GetCasesResponse>();
        return content!;
    }
}