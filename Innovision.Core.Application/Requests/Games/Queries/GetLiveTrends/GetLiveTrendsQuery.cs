using Innovision.Core.Application.Interfaces;
using Innovision.Core.Application.Requests.Players.Queries.GetSummary;
using Innovision.Core.Common.Interfaces;
using Innovision.Core.Domain.Enums;
using Innovision.Core.Infrastructure.Interfaces;
using MediatR;

namespace Innovision.Core.Application.Requests.Games.Queries.GetLiveTrends
{
    public record GetLiveTrendsQuery(Guid CompanyId) : IRequest<object>;
    public class GetLiveTrendsQueryHandler(ICoreDbContext coreDbContext, IGamesApi gameApi) : IRequestHandler<GetLiveTrendsQuery, object>
    {
        private readonly ICoreDbContext _coreDbContext = coreDbContext;
        private readonly IGamesApi _gameApi = gameApi;

        public async Task<object> Handle(GetLiveTrendsQuery request, CancellationToken cancellationToken)
        {

            var currentBetSchedule = await _gameApi.GetCurrentBetSchedule(request.CompanyId, cancellationToken);

            if (currentBetSchedule == null)
                return new { };

            if (currentBetSchedule == null)
                return new { };



            return new { };
        }
    }
}
