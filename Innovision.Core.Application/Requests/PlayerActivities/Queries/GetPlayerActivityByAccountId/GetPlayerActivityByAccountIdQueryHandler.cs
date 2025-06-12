using AutoMapper;
using AutoMapper.QueryableExtensions;
using Innovision.Core.Application.Common;
using Innovision.Core.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Innovision.Core.Application.Requests.PlayerActivities.Queries.GetPlayerActivityByAccountId
{
    public class GetPlayerActivityByAccountIdQueryHandler(ICoreDbContext dbContext, IMapper mapper) : IRequestHandler<GetPlayerActivityByAccountIdQuery, ApiResponse<PlayerActivityDto>>
    {
        private readonly ICoreDbContext _dbContext = dbContext;
        private readonly IMapper _mapper = mapper;

        public async Task<ApiResponse<PlayerActivityDto>> Handle(GetPlayerActivityByAccountIdQuery request, CancellationToken cancellationToken)
        {
            var activity = await _dbContext.PlayerActivities
                .Where(o => o.AccountInfoId == request.AccountId)
                .ProjectTo<PlayerActivityDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(cancellationToken);

            return new ApiResponse<PlayerActivityDto>() { Data = activity };
        }
    }
}
