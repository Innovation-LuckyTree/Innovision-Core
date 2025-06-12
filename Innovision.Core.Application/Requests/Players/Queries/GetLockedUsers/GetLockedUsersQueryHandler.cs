using AutoMapper;
using AutoMapper.QueryableExtensions;
using Innovision.Core.Application.Common;
using Innovision.Core.Application.Interfaces;
using Innovision.Core.Application.Requests.Users.Queries;
using Innovision.Core.Infrastructure.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Innovision.Core.Application.Requests.Players.Queries.GetLockedUsers;

public class GetLockedUsersQueryHandler(ICoreDbContext dbContext, IMapper mapper, ICoreIdentityApi coreIdentityApi) : IRequestHandler<GetLockedUsersQuery, ApiResponse<UserStatusVm>>
{
    private readonly ICoreDbContext _dbContext = dbContext;
    private readonly IMapper _mapper = mapper;
    private readonly ICoreIdentityApi _coreIdentityApi = coreIdentityApi;

    public async Task<ApiResponse<UserStatusVm>> Handle(GetLockedUsersQuery request, CancellationToken cancellationToken)
    {
        var result = await _coreIdentityApi.GetLockedUsers(request.CompanyObjectId,
            request.PagedQuery?.PageNumber,
            request.PagedQuery?.PageSize, cancellationToken);

        if (result == null)
            return new ApiResponse<UserStatusVm>() { Data = new UserStatusVm() };

        if (result.Results.Count == 0)
            return new ApiResponse<UserStatusVm>() { Data = new UserStatusVm() };

        var query = _dbContext.Accounts
            .Include(m => m.UserType)
            .Include(m => m.Branch)
            .Where(m => result.Results.Select(o => o.UserId).Contains(m.UserId))
            .OrderBy(x => x.AccountInfoId)
            .AsQueryable();

        var userslist = await query
            .ProjectTo<UserStatusDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        try
        {
            foreach (var item in userslist)
            {
                var luser = result.Results.Where(m => m.UserId == item.UserId).FirstOrDefault();
                item.LockedDate = (luser != null) ? luser.LockTime : null;
            }

            if (request.PagedQuery != null && !string.IsNullOrEmpty(request.PagedQuery.Search))
            {
                userslist = userslist.Where(m => m.Fullname.ToLower().Contains(request.PagedQuery.Search.ToLower())
                    || m.ContactNumber.ToLower().Contains(request.PagedQuery.Search.ToLower())).ToList();
            }

            return new ApiResponse<UserStatusVm>()
            {
                Data = new UserStatusVm
                {
                    Results = userslist,
                    Total = (request.PagedQuery != null && !string.IsNullOrEmpty(request.PagedQuery.Search)) ? userslist.Count : result.Total,
                    PageNumber = request.PagedQuery != null ? request.PagedQuery.PageNumber : 1,
                    PageSize = request.PagedQuery != null ? request.PagedQuery.PageSize : userslist.Count()
                }
            };
        }
        catch (Exception ex)
        {
            return new ApiResponse<UserStatusVm>() { Success = false, ErrorMessage = ex.Message };
        }
    }
}
