using Innovision.Core.Infrastructure.Interfaces;
using Innovision.Core.Infrastructure.Helpers;
using System.Net.Http.Json;
using Innovision.Core.Infrastructure.WebsocketServices.Models.Responses;
using Innovision.Core.Infrastructure.WebsocketServices.Models.Requests;

namespace Innovision.Core.Infrastructure.WebsocketServices;

public class WebsocketServicesApi : AbstractApiClient, IWebsocketServicesApi
{
    private readonly string _clientId;

    public WebsocketServicesApi(HttpClient? client, IAppConfig appConfig) : base(nameof(WebsocketServicesApi), client)
    {
        _client.BaseAddress = new Uri(appConfig.WebsocketApiClient.BaseAddressUrl);
        _client.DefaultRequestHeaders.Add("Resource", appConfig.WebsocketApiClient.Resource);

        _clientId = appConfig.AppId;
    }

    #region notification
    public async Task BlockUser(BlockUserRequest request, CancellationToken cancellationToken) =>
        await _client.PostAsJsonAsync($"/notification/isBlock", request, cancellationToken);

    public async Task FullyVerifiedUser(ApproveFullyVerifiedUserRequest request, CancellationToken cancellationToken) =>
        await _client.PostAsJsonAsync($"/notification/isFullyVerified", request, cancellationToken);

    public async Task AdminExclusion(CreateAdminExclusionRequest request, CancellationToken cancellationToken) =>
        await _client.PostAsJsonAsync($"/notification/isExcludedByAdmin", request, cancellationToken);

    public async Task<BroadcastNotificationCountResponse> PostNotificationAsync(BroadcastNotificationCountRequest request, CancellationToken cancellationToken)
    {
        var response = await _client.PostAsJsonAsync($"/notification/", request, cancellationToken);
        if (!response.IsSuccessStatusCode)
            throw new Exception("Connecting to websocket api was unsuccessful.");

        var content = await response.Content.ReadFromJsonAsync<BroadcastNotificationCountResponse>();
        return content!;
    }
    #endregion

    #region online
    public async Task<int> GetOnlineAccounts(CancellationToken cancellationToken)
    {
        var response = await _client.GetAsync($"/mobile/active", cancellationToken);
        if (!response.IsSuccessStatusCode)
            return 0;

        var content = await response.Content.ReadFromJsonAsync<OnlineCountResponse>(cancellationToken);
        return content.Count;
    }
    public async Task<OnlineListResponse> GetOnlinePlayers(int start, int size, CancellationToken cancellationToken)
    {
        var response = await _client.PostAsJsonAsync($"/mobile/active/accountids/list/", new { start = start, size = size }, cancellationToken);
        if (!response.IsSuccessStatusCode)
            return new OnlineListResponse();

        var content = await response.Content.ReadFromJsonAsync<OnlineListResponse>(cancellationToken);
        return content!;
    }

    public async Task<int> GetOnlineWithoutBetsCount(string drawTime, CancellationToken cancellationToken)
    {
        var response = await _client.PostAsJsonAsync($"/mobile/online/without-bets/count", new { drawTime = drawTime }, cancellationToken);
        if (!response.IsSuccessStatusCode)
            return 0;

        var content = await response.Content.ReadFromJsonAsync<OnlineWithoutBetsCountResponse>(cancellationToken);
        return content.Count;
    }

    public async Task<PlayerStatusResponse> GetPlayerStatus(long AccountInfoId, Guid UserId, CancellationToken cancellationToken)
    {
        var response = await _client.PostAsJsonAsync($"/mobile/player/status", new { accountInfoId = AccountInfoId, userId = UserId }, cancellationToken);
        if (!response.IsSuccessStatusCode)
            return new PlayerStatusResponse();

        var content = await response.Content.ReadFromJsonAsync<PlayerStatusResponse>(cancellationToken);
        return content;
    }
    #endregion
}
