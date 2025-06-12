using Innovision.Core.Infrastructure.WebsocketServices.Models.Requests;
using Innovision.Core.Infrastructure.WebsocketServices.Models.Responses;

namespace Innovision.Core.Infrastructure.Interfaces;

public interface IWebsocketServicesApi
{
    Task BlockUser(BlockUserRequest request, CancellationToken cancellationToken);
    Task FullyVerifiedUser(ApproveFullyVerifiedUserRequest request, CancellationToken cancellationToken);
    Task AdminExclusion(CreateAdminExclusionRequest request, CancellationToken cancellationToken);
    Task<BroadcastNotificationCountResponse> PostNotificationAsync(BroadcastNotificationCountRequest request, CancellationToken cancellationToken);
    Task<int> GetOnlineAccounts(CancellationToken cancellationToken);
    Task<OnlineListResponse> GetOnlinePlayers(int start, int size, CancellationToken cancellationToken);
    Task<int> GetOnlineWithoutBetsCount(string drawTime, CancellationToken cancellationToken);
    Task<PlayerStatusResponse> GetPlayerStatus(long AccountInfoId, Guid UserId, CancellationToken cancellationToken);
}
