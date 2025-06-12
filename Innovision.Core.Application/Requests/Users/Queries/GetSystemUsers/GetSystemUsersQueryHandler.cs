using AutoMapper;
using AutoMapper.QueryableExtensions;
using Innovision.Core.Application.Common;
using Innovision.Core.Application.Interfaces;
using Innovision.Core.Common.Interfaces;
using Innovision.Core.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Innovision.Core.Application.Requests.Users.Queries.GetSystemUsers;

public class GetSystemUsersQueryHandler : IRequestHandler<GetSystemUsersQuery, ApiResponse<List<SystemUserDto>>>
{
    private readonly ICoreDbContext _dbContext;
    private readonly IMapper _mapper;
    private readonly ICurrentUserService _currentUserService;

    public GetSystemUsersQueryHandler(ICoreDbContext dbContext, IMapper mapper, ICurrentUserService currentUserService)
    {
        _dbContext = dbContext;
        _mapper = mapper;
        _currentUserService = currentUserService;
    }

    public async Task<ApiResponse<List<SystemUserDto>>> Handle(GetSystemUsersQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var query = _dbContext.Accounts
                .Include(m => m.UserType)
                .Include(m => m.Branch)
                .Where(m => m.AccountStatusId == AccountStatus.Approved
                    || (m.AccountStatusId == AccountStatus.Migrated)
                    || (m.AccountStatusId == AccountStatus.Completed))
                .AsQueryable();

            if (request.BranchId != null)
                query = query.Where(m => m.BranchId == request.BranchId);

            if (request.RoleId != null)
                query = query.Where(m => m.UserType.UserTypeId == request.RoleId);

            if (request.IsDownline.HasValue && request.IsDownline.Value)
            {
                var account = _dbContext.Accounts.Where(x => x.UserId == _currentUserService.UserObjId).FirstOrDefault();

                if (account == null)
                    return new ApiResponse<List<SystemUserDto>>() { Success = false, ErrorMessage = "Account not found." };

                query = query.Where(m => m.RefferralCode == account.RefferralKey);
            }

            var userslist = await query
                .ProjectTo<SystemUserDto>(_mapper.ConfigurationProvider)
                .OrderByDescending(x => x.CreatedOn)
                .ToListAsync(cancellationToken);

            return new ApiResponse<List<SystemUserDto>>() { Data = userslist };
        }
        catch (Exception ex)
        {
            return new ApiResponse<List<SystemUserDto>>() { Success = false, ErrorMessage = ex.Message };
        }
    }
}
