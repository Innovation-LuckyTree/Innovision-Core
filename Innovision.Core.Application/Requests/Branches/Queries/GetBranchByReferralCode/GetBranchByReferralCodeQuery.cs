using AutoMapper;
using AutoMapper.QueryableExtensions;
using Innovision.Core.Application.Common;
using Innovision.Core.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Innovision.Core.Application.Requests.Branches.Queries.GetBranchByReferralCode
{
    public record GetBranchByReferralCodeQuery(string ReferralCode) : IRequest<ApiResponse<BranchInfoDto>>;

    public class GetBranchByReferralCodeQueryHandler : IRequestHandler<GetBranchByReferralCodeQuery, ApiResponse<BranchInfoDto>>
    {
        private readonly ICoreDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetBranchByReferralCodeQueryHandler(ICoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<ApiResponse<BranchInfoDto>> Handle(GetBranchByReferralCodeQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var branch = await _dbContext.Accounts
                    .Include(m => m.Branch)
                    .Where(m => m.RefferralKey == request.ReferralCode)
                    .Select(m => m.Branch)
                    .ProjectTo<BranchInfoDto>(_mapper.ConfigurationProvider)
                    .FirstOrDefaultAsync(cancellationToken);

                return new ApiResponse<BranchInfoDto>() { Data = branch };
            }
            catch (Exception ex)
            {
                return new ApiResponse<BranchInfoDto>() { Success = false, ErrorMessage = ex.Message };
            }
        }
    }
}