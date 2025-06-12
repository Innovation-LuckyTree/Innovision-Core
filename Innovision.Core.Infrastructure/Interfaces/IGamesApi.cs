using Innovision.Core.Infrastructure.Games.Models.Requests;
using Innovision.Core.Infrastructure.Games.Models.Responses;

namespace Innovision.Core.Infrastructure.Interfaces;

public interface IGamesApi
{
    Task<GetLuckyPickResponse> GetLuckyPickByGameId(int gameId, CancellationToken cancellationToken);
    Task<CurrentBetSummary> GetCurrentBetSummary(string companyId, CancellationToken cancellationToken);
    Task<PlayingNowResponse> GetCurrentBetUsers(PlayingNowRequest request, CancellationToken cancellationToken);
    Task<List<BetScheduleResponse>> GetCurrentBetSchedule(Guid CompanyObjectId, CancellationToken cancellationToken);
    Task<IEnumerable<AdvancedBetsResponse>> GetAdvancedBets(DateTime dateFrom, DateTime dateTo, CancellationToken cancellationToken);
    Task<object> GetLiveTrends(string gameScheduleIds, CancellationToken cancellationToken);
    Task<object> GetOpenCombination(string gameScheduleIds, string Search, int size, int start, CancellationToken cancellationToken);
    Task<object> GetCloseCombination(string gameScheduleIds, string Search, int size, int start, CancellationToken cancellationToken);
    Task<PlayingListResponse> GetCurrentBetPlayers(Guid companyId, int start, int size, CancellationToken cancellationToken);
}
