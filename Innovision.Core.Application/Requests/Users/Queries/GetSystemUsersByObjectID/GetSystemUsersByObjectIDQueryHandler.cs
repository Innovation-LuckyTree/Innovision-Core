using AutoMapper;
using AutoMapper.QueryableExtensions;
using Innovision.Core.Application.Common;
using Innovision.Core.Application.Interfaces;
using Innovision.Core.Infrastructure.CoreIdentity;
using Innovision.Core.Infrastructure.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Innovision.Core.Application.Requests.Users.Queries.GetSystemUsersByObjectID;

public class GetSystemUsersByObjectIDQueryHandler : IRequestHandler<GetSystemUsersByObjectIDQuery, ApiResponse<SystemUser>>
{
    private readonly ICoreDbContext _dbContext;
    private readonly IMapper _mapper;
    private readonly ICoreIdentityApi _coreIdentityApi;

    public GetSystemUsersByObjectIDQueryHandler(ICoreDbContext dbContext, IMapper mapper, ICoreIdentityApi coreIdentityApi)
    {
        _dbContext = dbContext;
        _mapper = mapper;
        _coreIdentityApi = coreIdentityApi;
    }

    public async Task<ApiResponse<SystemUser>> Handle(GetSystemUsersByObjectIDQuery request, CancellationToken cancellationToken)
    {
        var query = _dbContext.Accounts
                .Include(m => m.UserType)
                .Include(m => m.UserStatuses)
                .Include(m => m.AdministrativeExclusions)
                .Include(m => m.SelfLimits)
                .Include(m => m.BlockedUserHistories)
                .Include(m => m.Branch)
                .Where(m => m.AccountObjectId == request.AccountObjctId).AsQueryable();

        var userInfo = await query
            .ProjectTo<SystemUser>(_mapper.ConfigurationProvider)
            .OrderByDescending(x => x.CreatedOn)
            .FirstOrDefaultAsync(cancellationToken);

        try
        {
            var lockedUser = await _coreIdentityApi.GetLockedUser(userInfo.UserId, cancellationToken);
            userInfo.LockedUser = lockedUser;
        }
        catch (Exception ex)
        {
            userInfo.LockedUser = false;
        }

        return new ApiResponse<SystemUser>() { Data = userInfo };
    }
}
