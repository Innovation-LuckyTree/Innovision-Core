using AutoMapper;
using AutoMapper.QueryableExtensions;
using Innovision.Core.Application.Common;
using Innovision.Core.Application.Interfaces;
using Innovision.Core.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Innovision.Core.Application.Requests.Users.Queries.GetAgentsByBranchId
{
    public record GetAgentsByBranchIdQuery(int branchId, int userType) : IRequest<ApiResponse<List<UserBasicDto>>>;
    public class GetAgentsByBranchIdQueryHandler : IRequestHandler<GetAgentsByBranchIdQuery, ApiResponse<List<UserBasicDto>>>
    {
        private readonly ICoreDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetAgentsByBranchIdQueryHandler(ICoreDbContext dbContext, IMapper mapper)
        {
            _mapper = mapper;
            _dbContext = dbContext;
        }

        public async Task<ApiResponse<List<UserBasicDto>>> Handle(GetAgentsByBranchIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var query = _dbContext.Accounts.Where(x => x.UserTypeId == ((request.userType == 0) ? UserTypes.Agent : UserTypes.MasterAgent)
                        && x.BranchId == request.branchId
                        && (x.AccountStatusId == AccountStatus.Migrated
                            || x.AccountStatusId == AccountStatus.Completed)).AsQueryable();

                var resultlist = await query
                    .ProjectTo<UserBasicDto>(_mapper.ConfigurationProvider)
                    .OrderByDescending(x => x.FullName)
                    .ToListAsync(cancellationToken);

                return new ApiResponse<List<UserBasicDto>> { Data = resultlist };
            }
            catch (Exception ex)
            {
                return new ApiResponse<List<UserBasicDto>>() { Success = false, ErrorMessage = ex.Message };
            }
        }
    }
}
