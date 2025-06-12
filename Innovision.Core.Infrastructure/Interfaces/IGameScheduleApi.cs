using Innovision.Core.Infrastructure.GameSchedule.Models.Requests;
using Innovision.Core.Infrastructure.GameSchedule.Models.Responses;

namespace Innovision.Core.Infrastructure.Interfaces;

public interface IGameScheduleApi
{
    Task<IEnumerable<GameScheduleResponse>> GetGameScheduleListAsync(string companyId, CancellationToken cancellationToken);
    Task<GameScheduleResponse> GetGameScheduleByIdAsync(int gameScheduleId, CancellationToken cancellationToken);
    Task<T> GetGameCombinationPercentage<T>(int gameScheduleId, CancellationToken cancellationToken);
    Task<T> GetGameScheduleTotalAmountBet<T>(int gameScheduleId, CancellationToken cancellationToken);
    Task<GameScheduleResponse> AddGameScheduleAsync(GameScheduleRequest request, CancellationToken cancellationToken);
    Task DeleteGameScheduleAsync(int gameScheduleId, CancellationToken cancellationToken);
    Task<IEnumerable<GameDrawTypeResponse>> GetGameDrawTypesAsync(CancellationToken cancellationToken);
    Task<GameDrawTypeResponse> GetGameDrawTypeByIdAsync(int drawTypeId, CancellationToken cancellationToken);
    Task AddGameDrawTypeAsync(GameDrawTypeRequest request, CancellationToken cancellationToken);
    Task UpdateGameDrawTypeAsync(int gameTypeId, GameDrawTypeRequest request, CancellationToken cancellationToken);
    Task DeleteGameDrawTypeAsync(int gameTypeId, CancellationToken cancellationToken);
    Task<IEnumerable<ClosingDateResponse>> GetClosingDatesAsync(CancellationToken cancellationToken);
    Task<ClosingDateResponse> GetClosingDatesAsync(int closingDateId, CancellationToken cancellationToken);
    Task AddClosingDateAsync(ClosingDateRequest request, CancellationToken cancellationToken);
    Task DeleteClosingDateAsync(int closingDateId, CancellationToken cancellationToken);
}
