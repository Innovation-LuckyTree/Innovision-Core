using AutoMapper;
using AutoMapper.QueryableExtensions;
using Innovision.Core.Application.Common;
using Innovision.Core.Application.Interfaces;
using Innovision.Core.Common.Interfaces;
using Innovision.Core.Common.Models;
using Innovision.Core.Domain.Entity;
using Innovision.Core.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Innovision.Core.Application.Requests.Users.Queries.GetPaginatedUsers;

public class GetPaginatedUsersQueryHandler : IRequestHandler<GetPaginatedUsersQuery, ApiResponse<UserListVm>>
{
    private readonly ICoreDbContext _dbContext;
    private readonly IMapper _mapper;
    private readonly ICurrentUserService _currentUserService;

    public GetPaginatedUsersQueryHandler(ICoreDbContext dbContext, IMapper mapper, ICurrentUserService currentUserService)
    {
        _dbContext = dbContext;
        _mapper = mapper;
        _currentUserService = currentUserService;
    }

    public async Task<ApiResponse<UserListVm>> Handle(GetPaginatedUsersQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var query = _dbContext.Accounts
                .Include(m => m.UserType)
                .Include(m => m.Branch)
                .Where(m => m.AccountStatusId == AccountStatus.Approved
                    || (m.AccountStatusId == AccountStatus.Migrated)
                    || (m.AccountStatusId == AccountStatus.Completed))
                .OrderByDescending(x => x.CreatedOn)
                .AsQueryable();

            if (request.BranchId != null)
                query = query.Where(m => m.BranchId == request.BranchId);

            if (request.UserType != null)
                query = query.Where(m => m.UserType.UserTypeId == request.UserType);

            if (request.PagedQuery != null)
                query = GetPagedQueryOperator(query, request.PagedQuery);

            var totalCount = query.Count();

            var userslist = await query
                .ProjectTo<SystemUserDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);


            return new ApiResponse<UserListVm>()
            {
                Data = new UserListVm
                {
                    UserList = userslist,
                    Total = totalCount,
                    PageNumber = (request.PagedQuery != null) ? request.PagedQuery.PageNumber : 1,
                    PageSize = (request.PagedQuery != null) ? request.PagedQuery.PageSize : userslist.Count()
                }
            };
        }
        catch (Exception ex)
        {
            return new ApiResponse<UserListVm>() { Success = false, ErrorMessage = ex.Message };
        }
    }

    static public IQueryable<Account> GetPagedQueryOperator(IQueryable<Account> query, PagedQuery pagedQuery)
    {
        if (!string.IsNullOrEmpty(pagedQuery.Search))
            query = query.Where(q => (q.FirstName + " " + q.LastName).ToLower().Contains(pagedQuery.Search.ToLower()));

        if (pagedQuery.PageNumber >= 1)
            query = query.Skip(pagedQuery.PageNumber * pagedQuery.PageSize);

        query = query.Take(pagedQuery.PageSize);

        return query;
    }
}
