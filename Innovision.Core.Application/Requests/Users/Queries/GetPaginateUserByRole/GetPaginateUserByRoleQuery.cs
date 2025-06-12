using AutoMapper;
using AutoMapper.QueryableExtensions;
using Innovision.Core.Application.Common;
using Innovision.Core.Application.Common.Interfaces;
using Innovision.Core.Application.Interfaces;
using Innovision.Core.Common.Interfaces;
using Innovision.Core.Common.Models;
using Innovision.Core.Domain.Entity;
using Innovision.Core.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Innovision.Core.Application.Requests.Users.Queries.GetPaginateUserByRole
{
    public record GetPaginateUserByRoleQuery(List<int> RoleIds, Guid CompanyId, PagedQuery? PagedQuery) : IRequest<ApiResponse<SystemUserVm>>;
    public class GetPaginateUserByRoleQueryHandler(ICoreDbContext dbContext, IMapper mapper, ICurrentUserService currentUserService, ILoadingAccountServices loadingAccountServices)
        : IRequestHandler<GetPaginateUserByRoleQuery, ApiResponse<SystemUserVm>>
    {
        private readonly ICoreDbContext _dbContext = dbContext;
        private readonly IMapper _mapper = mapper;
        private readonly ICurrentUserService _currentUserService = currentUserService;
        private readonly ILoadingAccountServices _loadingAccountServices = loadingAccountServices;

        public async Task<ApiResponse<SystemUserVm>> Handle(GetPaginateUserByRoleQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var query = _dbContext.Accounts
                    .Include(m => m.Branch)
                    .Where(m => request.RoleIds.Contains(m.UserTypeId)
                        && (m.AccountStatusId == AccountStatus.Approved
                        || (m.AccountStatusId == AccountStatus.Migrated)
                        || (m.AccountStatusId == AccountStatus.Completed))
                        && m.IsActive)
                    .OrderByDescending(m => m.CreatedOn)
                    .AsQueryable();

                var totalCount = query.Count();

                if (request.PagedQuery != null)
                    query = QueryFilter(query, request.PagedQuery);

                var result = await query
                    .ProjectTo<SystemUserDto>(_mapper.ConfigurationProvider)
                    .ToListAsync(cancellationToken);

                return new ApiResponse<SystemUserVm> { Data = new SystemUserVm
                    {
                        Results = result,
                        Total = totalCount,
                        PageNumber = (request.PagedQuery != null) ? request.PagedQuery.PageNumber : 1,
                        PageSize = (request.PagedQuery != null) ? request.PagedQuery.PageSize : result.Count()
                    }
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse<SystemUserVm>() { Success = false, ErrorMessage = ex.Message };
            }
        }

        private IQueryable<Account> QueryFilter(IQueryable<Account> query, PagedQuery pagedQuery)
        {
            if (!string.IsNullOrEmpty(pagedQuery.Search))
                query = query.Where(q => (q.FirstName.ToLower() + " " + q.LastName.ToLower()).Contains(pagedQuery.Search.ToLower())
                || (q.MobileNumber.ToLower()).Contains(pagedQuery.Search.ToLower()));

            if (pagedQuery.PageNumber > 0)
                query = query.Skip((pagedQuery.PageNumber) * pagedQuery.PageSize);

            query = query.Take(pagedQuery.PageSize);

            return query;
        }
    }
}
