using AutoMapper;
using AutoMapper.QueryableExtensions;
using Innovision.Core.Application.Common;
using Innovision.Core.Application.Common.Contants;
using Innovision.Core.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Innovision.Core.Application.Requests.Players.Queries.GetPlayerByCompanyId
{
    public class GetPlayerByCompanyIdQueryHandler(ICoreDbContext coreDbContext, IMapper mapper) : IRequestHandler<GetPlayerByCompanyIdQuery, ApiResponse<List<CompanyPlayerDto>>>
    {
        private readonly ICoreDbContext _coreDbContext = coreDbContext;
        private readonly IMapper _mapper = mapper;

        public async Task<ApiResponse<List<CompanyPlayerDto>>> Handle(GetPlayerByCompanyIdQuery request, CancellationToken cancellationToken)
        {
            var players = await _coreDbContext.Accounts
                .Include(m => m.Branch)
                .Where(m => m.UserTypeId == UserContants.USER_TYPE_PLAYER)
                .ProjectTo<CompanyPlayerDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return new ApiResponse<List<CompanyPlayerDto>> { Data = players };
        }
    }
}
