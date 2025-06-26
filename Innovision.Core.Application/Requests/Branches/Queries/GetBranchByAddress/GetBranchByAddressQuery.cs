using System.ComponentModel.Design;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Innovision.Core.Application.Common;
using Innovision.Core.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Innovision.Core.Application.Requests.Branches.Queries.GetBranchByAddress
{
    public class GetBranchByAddressQuery : IRequest<ApiResponse<List<BranchInfoDto>>>
    {
        public string Region { get; set; }
        public string Province { get; set; }
        public string Municipality { get; set; }
    }

    public class GetBranchByAddressQueryHandler : IRequestHandler<GetBranchByAddressQuery, ApiResponse<List<BranchInfoDto>>>
    {
        private readonly ICoreDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetBranchByAddressQueryHandler(ICoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<ApiResponse<List<BranchInfoDto>>> Handle(GetBranchByAddressQuery request, CancellationToken cancellationToken)
        {
            List<BranchInfoDto> branches = new List<BranchInfoDto>();

            // where Region, Province and Municipality
            var resp = await _dbContext.Branches
                .Where(m => m.Address.Region == request.Region
                    && m.Address.Province == request.Province
                    && m.Address.Municipality == request.Municipality && !m.IsMain)
                .ProjectTo<BranchInfoDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
            branches = resp;


            if (resp.Count == 0)
            {
                // where Region and Province
                var resp1 = await _dbContext.Branches
                    .Where(m => m.Address.Region == request.Region
                        && m.Address.Province == request.Province && !m.IsMain)
                    .ProjectTo<BranchInfoDto>(_mapper.ConfigurationProvider)
                    .ToListAsync(cancellationToken);
                branches = resp1;

                if (resp1.Count == 0)
                {
                    // where Region
                    var resp2 = await _dbContext.Branches
                        .Where(m => m.Address.Region == request.Region && !m.IsMain)
                        .ProjectTo<BranchInfoDto>(_mapper.ConfigurationProvider)
                        .ToListAsync(cancellationToken);
                    branches = resp2;

                    if (resp2.Count == 0)
                    {
                        var resp3 = await _dbContext.Branches
                        .Where(m => !m.IsMain)
                        .Take(5)
                        .ProjectTo<BranchInfoDto>(_mapper.ConfigurationProvider)
                        .ToListAsync(cancellationToken);
                        branches = resp3;
                    }
                }
            }

            return new ApiResponse<List<BranchInfoDto>>() { Data = branches };
        }
    }
}
