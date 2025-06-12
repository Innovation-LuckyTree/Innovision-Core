using AutoMapper;
using AutoMapper.QueryableExtensions;
using Innovision.Core.Application.Common;
using Innovision.Core.Application.Interfaces;
using Innovision.Core.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Innovision.Core.Application.Requests.Users.Queries.GetVerifiedUsers
{
    public record GetVerifiedUsersQuery(int? companyId) : IRequest<ApiResponse<List<VerifiedUserDto>>>;
    public class GetVerifiedUsersQueryHandler : IRequestHandler<GetVerifiedUsersQuery, ApiResponse<List<VerifiedUserDto>>>
    {
        private readonly ICoreDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetVerifiedUsersQueryHandler(ICoreDbContext dbContext, IMapper mapper)
        {
            _mapper = mapper;
            _dbContext = dbContext;
        }

        public async Task<ApiResponse<List<VerifiedUserDto>>> Handle(GetVerifiedUsersQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var query = _dbContext.Accounts.Where(x => x.IsVerified
                        && (x.AccountStatusId == AccountStatus.Migrated
                            || x.AccountStatusId == AccountStatus.Completed)
                        && (x.UserTypeId == UserTypes.MasterAgent
                            || x.UserTypeId == UserTypes.Agent
                            || x.UserTypeId == UserTypes.Player))
                    .OrderByDescending(x => x.AccountInfoId)
                    .AsQueryable();

                var resultlist = await query
                    .ProjectTo<VerifiedUserDto>(_mapper.ConfigurationProvider)
                    .ToListAsync(cancellationToken);

                return new ApiResponse<List<VerifiedUserDto>> { Data = resultlist  };
            }
            catch (Exception ex)
            {
                return new ApiResponse<List<VerifiedUserDto>>() { Success = false, ErrorMessage = ex.Message };
            }
        }
    }
}
