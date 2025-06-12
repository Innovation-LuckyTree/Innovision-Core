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

namespace Innovision.Core.Application.Requests.Users.AccountApproval.Queries.GetUsersForApprove;

public class GetUsersForApproveQuery() : IRequest<ApiResponse<UsersForApprovedVm>>
{
    public PagedQuery? PagedQuery { get; set; }
}

public class GetUsersForApproveQueryHandler : IRequestHandler<GetUsersForApproveQuery, ApiResponse<UsersForApprovedVm>>
{
    private readonly ICoreDbContext _dbContext;
    private readonly IMapper _mapper;
    private readonly ICurrentUserService _currentUserService;

    public GetUsersForApproveQueryHandler(ICoreDbContext dbContext, IMapper mapper, ICurrentUserService currentUserService)
    {
        _dbContext = dbContext;
        _mapper = mapper;
        _currentUserService = currentUserService;
    }

    public async Task<ApiResponse<UsersForApprovedVm>> Handle(GetUsersForApproveQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var account = _dbContext.Accounts.Where(x => x.UserId == _currentUserService.UserObjId).FirstOrDefault();

            if (account == null)
                return new ApiResponse<UsersForApprovedVm>() { Success = false, ErrorMessage = "Account not found." };

            var query = _dbContext.Accounts.Where(m => m.RefferralCode == account.RefferralKey
                //&& m.UserTypeId == UserTypes.NewRegister
                //&& m.UserTypeId != UserTypes.MasterAgent
                && m.UserTypeId != UserTypes.Agent
                //&& m.UserTypeId != UserTypes.Player
                && m.AccountStatusId != AccountStatus.Declined).AsQueryable();

            var totalCount = query.Count();

            if (request.PagedQuery != null)
                query = QueryFilter(query, request.PagedQuery);

            var queryResults = await query
                .ProjectTo<UsersForApprovedDto>(_mapper.ConfigurationProvider)
                .OrderByDescending(x => x.CreatedOn)
                .ToListAsync(cancellationToken);

            return new ApiResponse<UsersForApprovedVm>()
            {
                Data = new UsersForApprovedVm
                {
                    Results = queryResults,
                    Total = totalCount,
                    PageNumber = (request.PagedQuery != null) ? request.PagedQuery.PageNumber : 1,
                    PageSize = (request.PagedQuery != null) ? request.PagedQuery.PageSize : queryResults.Count()
                },
            };
        }
        catch (Exception ex)
        {
            return new ApiResponse<UsersForApprovedVm>() { Success = false, ErrorMessage = ex.Message };
        }
    }

    public IQueryable<Account> QueryFilter(IQueryable<Account> query, PagedQuery pagedQuery)
    {
        if (!string.IsNullOrEmpty(pagedQuery.Search))
            query = query.Where(q => (q.FirstName.ToLower() + " " + q.LastName.ToLower()).Contains(pagedQuery.Search.ToLower()));

        if (pagedQuery.PageNumber > 0)
            query = query.Skip((pagedQuery.PageNumber) * pagedQuery.PageSize);

        query = query.Take(pagedQuery.PageSize);

        return query;
    }
}
