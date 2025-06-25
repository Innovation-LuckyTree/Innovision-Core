using AutoMapper;
using AutoMapper.QueryableExtensions;
using Innovision.Core.Application.Common;
using Innovision.Core.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Innovision.Core.Application.Requests.Branches.Queries.GetBranchByAddress
{
    public class GetBranchByAddressQueryV2Handler : IRequestHandler<GetBranchByAddressQueryV2, ApiResponse<List<BranchInfoDto>>>
    {
        private readonly ICoreDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetBranchByAddressQueryV2Handler(ICoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<ApiResponse<List<BranchInfoDto>>> Handle(GetBranchByAddressQueryV2 request, CancellationToken cancellationToken)
        {
            List<BranchInfoDto> branches = new List<BranchInfoDto>();

            // where Region, Province and Municipality
            var resp = await _dbContext.Branches
                .Where(m => m.Address.Region == request.Region
                    && m.Address.Province == request.Province && !m.IsMain)
                .ProjectTo<BranchInfoDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
            branches = resp;


            if (resp.Count == 0)
            {
                // where Region
                var resp1 = await _dbContext.Branches
                    .Where(m => m.Address.Region == request.Region && !m.IsMain)
                    .ProjectTo<BranchInfoDto>(_mapper.ConfigurationProvider)
                    .ToListAsync(cancellationToken);
                branches = resp1;

                if (resp1.Count == 0)
                {
                    var resp2 = await _dbContext.Branches
                    .Where(m => !m.IsMain)
                    .Take(5)
                    .ProjectTo<BranchInfoDto>(_mapper.ConfigurationProvider)
                    .ToListAsync(cancellationToken);
                    branches = resp2;
                }
            }


            return new ApiResponse<List<BranchInfoDto>>() { Data = branches };
        }
    }
}
