using System.Globalization;
using Innovision.Core.Application.Interfaces;
using Innovision.Core.Common.Interfaces;
using Innovision.Core.Domain.Enums;
using Innovision.Core.Infrastructure.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Innovision.Core.Application.Requests.Players.Queries.GetSummary;

public class GetSummaryQueryHandler(ICoreDbContext coreDbContext,
    IGamesApi gameApi, ICurrentUserService currentUserService,
    IWebsocketServicesApi websocketServicesApi) : IRequestHandler<GetSummaryQuery, CurrentBetsSummary>
{
    private readonly ICoreDbContext _coreDbContext = coreDbContext;
    private readonly IGamesApi _gameApi = gameApi;
    private readonly ICurrentUserService _currentUserService = currentUserService;
    private readonly IWebsocketServicesApi _websocketServicesApi = websocketServicesApi;

    public async Task<CurrentBetsSummary> Handle(GetSummaryQuery request, CancellationToken cancellationToken)
    {
        CurrentBetsSummary currentBetsSummary = new();

        var gameSummary = await _gameApi.GetCurrentBetSummary(_currentUserService.CompanyId, cancellationToken);
        var onlineCount = await _websocketServicesApi.GetOnlineAccounts(cancellationToken);

        Guid companyId = Guid.Parse(_currentUserService.CompanyId);
        var gameSchedules = await _gameApi.GetCurrentBetSchedule(companyId, cancellationToken);
        var firstGameSchedule = gameSchedules[0];
        TimeSpan drawTime = firstGameSchedule.DrawTime;
        DateTime drawDateTime = DateTime.Today.Add(drawTime);
        string drawTimeFormatted = drawDateTime.ToString("htt", CultureInfo.InvariantCulture).ToUpper();
        var onlineWithoutBetCount = await _websocketServicesApi.GetOnlineWithoutBetsCount(drawTimeFormatted, cancellationToken);

        var totalPlayers = await _coreDbContext.Accounts.Where(o => o.UserTypeId == UserTypes.Player && o.AccountStatusId == AccountStatus.Completed)
            .CountAsync(cancellationToken);

        currentBetsSummary.TotalPlayer = totalPlayers;
        currentBetsSummary.ActivePlayer = gameSummary.UserCount;
        currentBetsSummary.TotalBetAmount = gameSummary.TotalBetAmount;
        currentBetsSummary.OnlinePlayer = onlineCount;
        currentBetsSummary.OnlinePlayerWithoutBet = onlineWithoutBetCount;

        return currentBetsSummary;
    }
}