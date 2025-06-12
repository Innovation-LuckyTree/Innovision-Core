using AutoMapper;
using AutoMapper.QueryableExtensions;
using Innovision.Core.Application.Common;
using Innovision.Core.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Innovision.Core.Application.Requests.Branches.Queries.GetMainBranch
{
    public record GetMainBranchQuery(Guid CompanyObjectId) : IRequest<ApiResponse<BranchDto>>;
    public class GetMainBranchQueryHandler(IMapper mapper, ICoreDbContext dbContext) : IRequestHandler<GetMainBranchQuery, ApiResponse<BranchDto>>
    {
        private readonly IMapper _mapper = mapper;
        private readonly ICoreDbContext _dbContext = dbContext;

        public async Task<ApiResponse<BranchDto>> Handle(GetMainBranchQuery request, CancellationToken cancellationToken)
        {
            var branchQuery = _dbContext.Branches
                .Include(o => o.Account)
                .Where(m => m.BranchId != -1 && m.IsMain)
                .AsQueryable();

            var branch = await branchQuery
                .ProjectTo<BranchDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(cancellationToken);

            return new ApiResponse<BranchDto> { Data = branch };
        }
    }
}
