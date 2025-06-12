using Innovision.Core.Infrastructure.Interfaces;
using Innovision.Core.Infrastructure.WebsocketServices.Models.Responses;
using MediatR;

namespace Innovision.Core.Application.Requests.Players.Queries.GetPlayerStatus
{
    public record GetPlayerStatusQuery(long AccountInfoId, Guid UserId) : IRequest<PlayerStatusResponse>;
    public class GetPlayerStatusQueryHandler(IWebsocketServicesApi websocketServicesApi) : IRequestHandler<GetPlayerStatusQuery, PlayerStatusResponse>
    {
        private readonly IWebsocketServicesApi _websocketServicesApi = websocketServicesApi;

        public async Task<PlayerStatusResponse> Handle(GetPlayerStatusQuery request, CancellationToken cancellationToken)
        {
            return await _websocketServicesApi.GetPlayerStatus(request.AccountInfoId, request.UserId, cancellationToken);
        }
    }
}
