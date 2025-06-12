using Innovision.Core.Infrastructure.Interfaces;
using Innovision.Core.Infrastructure.Helpers;
using System.Net.Http.Json;
using Innovision.Core.Infrastructure.Games.Models.Responses;
using Innovision.Core.Infrastructure.Games.Models.Requests;

namespace Innovision.Core.Infrastructure.Games;

public class GamesApi : AbstractApiClient, IGamesApi
{
    private readonly string _clientId;

    public GamesApi(HttpClient? client, IAppConfig appConfig) : base(nameof(GamesApi), client)
    {
        _client.BaseAddress = new Uri(appConfig.GameApiClient.BaseAddressUrl);
        _client.DefaultRequestHeaders.Add("Resource", appConfig.GameApiClient.Resource);

        _clientId = appConfig.AppId;
    }

    #region Game reference
    public async Task<GetLuckyPickResponse> GetLuckyPickByGameId(int gameId, CancellationToken cancellationToken)
    {
        var response = await _client.GetAsync($"/game/{gameId}/lucky-pick/", cancellationToken);
        if (!response.IsSuccessStatusCode)
            throw new Exception("Connecting to core api was unsuccessful.");

        var content = await response.Content.ReadFromJsonAsync<GetLuckyPickResponse>();
        return content!;
    }

    public async Task<GameResponse> GetGameByIdAsync(int gameId, CancellationToken cancellationToken)
    {
        var response = await _client.GetAsync($"/game/{gameId}/", cancellationToken);
        if (!response.IsSuccessStatusCode)
            throw new Exception("Connecting to core api was unsuccessful.");

        var content = await response.Content.ReadFromJsonAsync<GameResponse>();
        return content!;
    }

    public async Task<GameResponse> CreateGameAsync(CreateGameRequest request, CancellationToken cancellationToken)
    {
        var response = await _client.PostAsJsonAsync($"/game/", request, cancellationToken);
        if (!response.IsSuccessStatusCode)
            throw new Exception("Connecting to core api was unsuccessful.");

        var content = await response.Content.ReadFromJsonAsync<GameResponse>();
        return content!;
    }
    #endregion

    #region Company Game
    public async Task<IEnumerable<CompanyGameResponse>> GetCompanyGameListAsync(CancellationToken cancellationToken)
    {
        var response = await _client.GetAsync("/company-game/", cancellationToken);
        if (!response.IsSuccessStatusCode)
            throw new Exception("Connecting to core api was unsuccessful.");

        var content = await response.Content.ReadFromJsonAsync<IEnumerable<CompanyGameResponse>>();
        return content!;
    }

    public async Task<CompanyGameResponse> GetCompanyGameByIdAsync(int companyGameId, CancellationToken cancellationToken)
    {
        var response = await _client.GetAsync($"/company-game/{companyGameId}/", cancellationToken);
        if (!response.IsSuccessStatusCode)
            throw new Exception("Connecting to core api was unsuccessful.");

        var content = await response.Content.ReadFromJsonAsync<CompanyGameResponse>();
        return content!;
    }

    public async Task<CompanyGameResponse> CreateCompanyGameAsync(CompanyGameRequest request, CancellationToken cancellationToken)
    {
        var response = await _client.PutAsJsonAsync($"/company-game/", request, cancellationToken);
        if (!response.IsSuccessStatusCode)
            throw new Exception("Connecting to core api was unsuccessful.");

        var content = await response.Content.ReadFromJsonAsync<CompanyGameResponse>();
        return content!;
    }

    public async Task<CompanyGameResponse> UpdateCompanyGameAsync(CompanyGameRequest request, CancellationToken cancellationToken)
    {
        var response = await _client.PostAsJsonAsync($"/company-game/", request, cancellationToken);
        if (!response.IsSuccessStatusCode)
            throw new Exception("Connecting to core api was unsuccessful.");

        var content = await response.Content.ReadFromJsonAsync<CompanyGameResponse>(cancellationToken);
        return content!;
    }
    #endregion

    #region Bet Transaction
    public async Task<CurrentBetSummary> GetCurrentBetSummary(string companyId, CancellationToken cancellationToken)
    {
        var response = await _client.GetAsync($"/bet-transaction/{companyId}/current-bet-summary/", cancellationToken);
        if (!response.IsSuccessStatusCode)
        {
            return new CurrentBetSummary();
        }

        var content = await response.Content.ReadFromJsonAsync<CurrentBetSummary>(cancellationToken);
        return content!;
    }

    public async Task<PlayingNowResponse> GetCurrentBetUsers(PlayingNowRequest request, CancellationToken cancellationToken)
    {
        var response = await _client.PostAsJsonAsync($"/bet-transaction/current/bets/", request, cancellationToken);
        if (!response.IsSuccessStatusCode)
        {
            return new PlayingNowResponse();
        }
        var content = await response.Content.ReadFromJsonAsync<PlayingNowResponse>(cancellationToken);
        return content!;
    }

    public async Task<IEnumerable<AdvancedBetsResponse>> GetAdvancedBets(DateTime dateFrom, DateTime dateTo, CancellationToken cancellationToken)
    {
        var response = await _client.GetAsync($"/bet-item/advanced-transactions/?dateFrom={dateFrom:yyyy-MM-dd}&dateTo={dateTo:yyyy-MM-dd}", cancellationToken);
        if (!response.IsSuccessStatusCode)
        {
            return [];
        }
        var content = await response.Content.ReadFromJsonAsync<IEnumerable<AdvancedBetsResponse>>(cancellationToken);
        return content!;
    }

    public async Task<List<BetScheduleResponse>> GetCurrentBetSchedule(Guid CompanyObjectId, CancellationToken cancellationToken)
    {
        var response = await _client.GetAsync($"/game-schedule/company-current-bet/?companyId={CompanyObjectId}", cancellationToken);
        var content = await response.Content.ReadFromJsonAsync<List<BetScheduleResponse>>(cancellationToken);
        return content!;
    }

    //{
    //  "totalIncomingBets": 0,
    //  "totalOutgoingBets": 0,
    //  "totalGross": 0
    //}
    public async Task<object> GetLiveTrends(string gameScheduleIds, CancellationToken cancellationToken)
    {
        var response = await _client.GetAsync($"/bet-item/get-live-trends-data/?gameScheduleIds={gameScheduleIds}", cancellationToken);
        var content = await response.Content.ReadFromJsonAsync<object>(cancellationToken);
        return content!;
    }

    public async Task<object> GetOpenCombination(string gameScheduleIds, string Search, int size, int start, CancellationToken cancellationToken)
    {
        var response = await _client.GetAsync($"/bet-item/open-combinations/?gameScheduleIds={gameScheduleIds}&size={size}&start={start}&search={Search}", cancellationToken);
        var content = await response.Content.ReadFromJsonAsync<object>(cancellationToken);
        return content!;
    }

    public async Task<object> GetCloseCombination(string gameScheduleIds, string Search, int size, int start, CancellationToken cancellationToken)
    {
        var response = await _client.GetAsync($"/soldout-combination/list/?gameScheduleIds={gameScheduleIds}&size={size}&start={start}&search={Search}", cancellationToken);
        var content = await response.Content.ReadFromJsonAsync<object>(cancellationToken);
        return content!;
    }

    public async Task<PlayingListResponse> GetCurrentBetPlayers(Guid companyId, int start, int size, CancellationToken cancellationToken)
    {
        var response = await _client.PostAsJsonAsync($"/bet-item/current/bets/accountids/", new { companyId = companyId, start = start, size = size }, cancellationToken);
        if (!response.IsSuccessStatusCode)
        {
            return new PlayingListResponse();
        }
        var content = await response.Content.ReadFromJsonAsync<PlayingListResponse>(cancellationToken);
        return content!;
    }
    #endregion
}
