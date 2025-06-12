using Innovision.Core.Application.Common.Interfaces;
using Innovision.Core.Infrastructure.Interfaces;

namespace Innovision.Core.Application.Common.Services;

public class UserStatusServices(IWebsocketServicesApi websocketServicesApi, IGamesApi gamesApi) : IUserStatusServices
{
    private readonly IWebsocketServicesApi _websocketServicesApi = websocketServicesApi;
    private readonly IGamesApi _gamesApi = gamesApi;

    public async Task<List<long>> GetOnlineIds(CancellationToken cancellationToken)
    {
        List<long> idList = [];

        var onlineIds = await _websocketServicesApi.GetOnlinePlayers(0, 1000, cancellationToken);

        if ((onlineIds?.Data?.Count ?? 0) == 0)
            return [];

        var dataList = onlineIds.Data.Where(o => o != null).Select(o => o.Value).ToList();

        if (dataList.Count > 0)
            idList.AddRange(dataList);

        // query next page by 1000
        if (onlineIds.TotalCount > 1000)
        {
            // round if any decimal point
            var lprange = Math.Round(Convert.ToDecimal(onlineIds.TotalCount / 1000));
            var range = ((lprange % 1) == 0) ? lprange : (lprange + 1);
            for (int i = 1; i < range; i++)
            {
                var onlineGuids1 = await _websocketServicesApi.GetOnlinePlayers(i, 1000, cancellationToken);
                if ((onlineGuids1?.Data?.Count() ?? 0) == 0)
                    continue;

                var onlineData = onlineIds.Data.Where(o => o != null).Select(o => o.Value).ToList();
                if (onlineData.Count > 0)
                    idList.AddRange(onlineData);
            }
        }

        return idList;
    }

    public async Task<List<long>> GetPlayingIds(Guid CompanyObjId, CancellationToken cancellationToken)
    {
        List<long> idList = [];
        var playingUsers = await _gamesApi.GetCurrentBetPlayers(CompanyObjId, 0, 1000, cancellationToken);

        if ((playingUsers?.Data?.AccountIds?.Count ?? 0) == 0)
            return [];

        idList.AddRange(playingUsers.Data.AccountIds);

        // query next page by 1000
        if (playingUsers.Total > 1000)
        {
            // round if any decimal point
            var lprange = Math.Round(Convert.ToDecimal(playingUsers.Total / 1000));
            var range = ((lprange % 1) == 0) ? lprange : (lprange + 1);
            for (int i = 1; i < range; i++)
            {
                var onlineGuids1 = await _gamesApi.GetCurrentBetPlayers(CompanyObjId, i, 1000, cancellationToken);

                if ((onlineGuids1?.Data?.AccountIds?.Count ?? 0) == 0)
                    continue;

                idList.AddRange(onlineGuids1.Data.AccountIds);
            }
        }
        return idList;
    }
}
