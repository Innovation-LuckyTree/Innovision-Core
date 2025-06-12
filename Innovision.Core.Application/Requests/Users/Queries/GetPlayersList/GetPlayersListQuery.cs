using AutoMapper;
using AutoMapper.QueryableExtensions;
using Innovision.Core.Application.Common;
using Innovision.Core.Application.Interfaces;
using Innovision.Core.Application.Requests.Users.Queries.GetVerifiedUsers;
using Innovision.Core.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Innovision.Core.Application.Requests.Users.Queries.GetPlayersList
{
    public record GetPlayersListQuery(int? companyId) : IRequest<ApiResponse<List<VerifiedUserDto>>>;
    public class GetPlayersListQueryHandler : IRequestHandler<GetPlayersListQuery, ApiResponse<List<VerifiedUserDto>>>
    {
        private readonly ICoreDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetPlayersListQueryHandler(ICoreDbContext dbContext, IMapper mapper)
        {
            _mapper = mapper;
            _dbContext = dbContext;
        }

        public async Task<ApiResponse<List<VerifiedUserDto>>> Handle(GetPlayersListQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var query = _dbContext.Accounts.Include(m => m.UserType)
                    .Where(x => x.UserTypeId == UserTypes.Player
                        //|| x.UserTypeId == UserTypes.Operator
                        //|| x.UserTypeId == UserTypes.MasterAgent
                        //|| x.UserTypeId == UserTypes.Agent
                        && (x.AccountStatusId == AccountStatus.Migrated
                            || x.AccountStatusId == AccountStatus.Completed)).AsQueryable();

                var resultlist = await query
                    .ProjectTo<VerifiedUserDto>(_mapper.ConfigurationProvider)
                    .OrderByDescending(x => x.FullName)
                    .ToListAsync(cancellationToken);

                // Get Agent name
                foreach (var item in resultlist)
                {
                    var upline = await _dbContext.Accounts.Where(m => m.RefferralKey == item.RefferralCode).FirstOrDefaultAsync(cancellationToken);
                    item.RecruiterName = (upline != null) ? $"{upline.FirstName} {upline.LastName}" : "";
                }

                return new ApiResponse<List<VerifiedUserDto>> { Data = resultlist };
            }
            catch (Exception ex)
            {
                return new ApiResponse<List<VerifiedUserDto>>() { Success = false, ErrorMessage = ex.Message };
            }
        }
    }
}
