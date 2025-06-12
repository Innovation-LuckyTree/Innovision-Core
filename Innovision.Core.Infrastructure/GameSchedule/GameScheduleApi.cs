using Innovision.Core.Infrastructure.Interfaces;
using Innovision.Core.Infrastructure.Helpers;
using Innovision.Core.Infrastructure.GameSchedule.Models.Responses;
using Innovision.Core.Infrastructure.GameSchedule.Models.Requests;
using System.Net.Http.Json;

namespace Innovision.Core.Infrastructure.GameSchedule;

public class GameScheduleApi : AbstractApiClient, IGameScheduleApi
{
    private readonly string _clientId;

    public GameScheduleApi(HttpClient? client, IAppConfig appConfig) : base(nameof(GameScheduleApi), client)
    {
        _client.BaseAddress = new Uri(appConfig.GameApiClient.BaseAddressUrl);
        _client.DefaultRequestHeaders.Add("Resource", appConfig.GameApiClient.Resource);

        _clientId = appConfig.AppId;
    }

    #region Game Schedule
    public async Task<IEnumerable<GameScheduleResponse>> GetGameScheduleListAsync(string companyId, CancellationToken cancellationToken)
    {
        var response = await _client.GetAsync($"/game-schedule/{companyId}", cancellationToken);
        if (!response.IsSuccessStatusCode)
            throw new Exception("Connecting to core api was unsuccessful.");

        var content = await response.Content.ReadFromJsonAsync<IEnumerable<GameScheduleResponse>>();
        return content!;
    }

    public async Task<GameScheduleResponse> GetGameScheduleByIdAsync(int gameScheduleId, CancellationToken cancellationToken)
    {
        var response = await _client.GetAsync($"/game-schedule/{gameScheduleId}/", cancellationToken);
        if (!response.IsSuccessStatusCode)
            throw new Exception("Connecting to core api was unsuccessful.");

        var content = await response.Content.ReadFromJsonAsync<GameScheduleResponse>();
        return content!;
    }

    public async Task<T> GetGameCombinationPercentage<T>(int gameScheduleId, CancellationToken cancellationToken)
    {
        var response = await _client.GetAsync($"/game-schedule/{gameScheduleId}/combination-percentage/", cancellationToken);
        if (!response.IsSuccessStatusCode)
            throw new Exception("Connecting to core api was unsuccessful.");

        var content = await response.Content.ReadFromJsonAsync<T>();
        return content!;
    }

    public async Task<T> GetGameScheduleTotalAmountBet<T>(int gameScheduleId, CancellationToken cancellationToken)
    {
        var response = await _client.GetAsync($"/game-schedule/{gameScheduleId}/total-bet-amount/", cancellationToken);
        if (!response.IsSuccessStatusCode)
            throw new Exception("Connecting to core api was unsuccessful.");

        var content = await response.Content.ReadFromJsonAsync<T>();
        return content!;
    }

    public async Task<GameScheduleResponse> AddGameScheduleAsync(GameScheduleRequest request, CancellationToken cancellationToken)
    {
        var response = await _client.PostAsJsonAsync($"/game-schedule/", request, cancellationToken);
        if (!response.IsSuccessStatusCode)
            throw new Exception("Connecting to core api was unsuccessful.");

        var content = await response.Content.ReadFromJsonAsync<GameScheduleResponse>();
        return content!;
    }

    public async Task DeleteGameScheduleAsync(int gameScheduleId, CancellationToken cancellationToken)
    {
        var response = await _client.DeleteAsync($"/game-schedule/{gameScheduleId}/delete/", cancellationToken);

        response.EnsureSuccessStatusCode();
    }
    #endregion

    #region Draw Types
    public async Task<IEnumerable<GameDrawTypeResponse>> GetGameDrawTypesAsync(CancellationToken cancellationToken)
    {
        var response = await _client.GetAsync("/game-draw-type/", cancellationToken);
        if (!response.IsSuccessStatusCode)
            throw new Exception("Connecting to core api was unsuccessful.");

        var content = await response.Content.ReadFromJsonAsync<IEnumerable<GameDrawTypeResponse>>();
        return content!;
    }

    public async Task<GameDrawTypeResponse> GetGameDrawTypeByIdAsync(int drawTypeId, CancellationToken cancellationToken)
    {
        var response = await _client.GetAsync($"/game-draw-type/{drawTypeId}/", cancellationToken);
        if (!response.IsSuccessStatusCode)
            throw new Exception("Connecting to core api was unsuccessful.");

        var content = await response.Content.ReadFromJsonAsync<GameDrawTypeResponse>();
        return content!;
    }

    public async Task AddGameDrawTypeAsync(GameDrawTypeRequest request, CancellationToken cancellationToken)
    {
        var response = await _client.PostAsJsonAsync("/game-draw-type/", request, cancellationToken);

        response.EnsureSuccessStatusCode();
    }

    public async Task UpdateGameDrawTypeAsync(int gameTypeId, GameDrawTypeRequest request, CancellationToken cancellationToken)
    {
        var response = await _client.PutAsJsonAsync($"/game-draw-type/{gameTypeId}/", request, cancellationToken);

        response.EnsureSuccessStatusCode();
    }

    public async Task DeleteGameDrawTypeAsync(int gameTypeId, CancellationToken cancellationToken)
    {
        var response = await _client.DeleteAsync($"/game-draw-type/{gameTypeId}/delete", cancellationToken);

        response.EnsureSuccessStatusCode();
    }
    #endregion

    #region Closing Dates
    public async Task<IEnumerable<ClosingDateResponse>> GetClosingDatesAsync(CancellationToken cancellationToken)
    {
        var response = await _client.GetAsync("/closed-date/", cancellationToken);
        if (!response.IsSuccessStatusCode)
            throw new Exception("Connecting to core api was unsuccessful.");

        var content = await response.Content.ReadFromJsonAsync<IEnumerable<ClosingDateResponse>>();
        return content!;
    }

    public async Task<ClosingDateResponse> GetClosingDatesAsync(int closingDateId, CancellationToken cancellationToken)
    {
        var response = await _client.GetAsync($"/closed-date/{closingDateId}/", cancellationToken);
        if (!response.IsSuccessStatusCode)
            throw new Exception("Connecting to core api was unsuccessful.");

        var content = await response.Content.ReadFromJsonAsync<ClosingDateResponse>();
        return content!;
    }

    public async Task AddClosingDateAsync(ClosingDateRequest request, CancellationToken cancellationToken)
    {
        var response = await _client.PostAsJsonAsync($"/closed-date/", request, cancellationToken);
        if (!response.IsSuccessStatusCode)
            throw new Exception("Connecting to core api was unsuccessful.");

        response.EnsureSuccessStatusCode();
    }

    public async Task DeleteClosingDateAsync(int closingDateId, CancellationToken cancellationToken)
    {
        var response = await _client.DeleteAsync($"/closed-date/{closingDateId}/", cancellationToken);
        if (!response.IsSuccessStatusCode)
            throw new Exception("Connecting to core api was unsuccessful.");

        response.EnsureSuccessStatusCode();
    }
    #endregion
}
