using AutoMapper;
using AutoMapper.QueryableExtensions;
using Innovision.Core.Application.Common;
using Innovision.Core.Application.Interfaces;
using Innovision.Core.Domain.Entity;
using Innovision.Core.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Innovision.Core.Application.Requests.Users.Queries.GetUserForVerification;

public class GetUserForVerificationQueryHandler : IRequestHandler<GetUserForVerificationQuery, ApiResponse<UsersVerificationVm>>
{
    private readonly ICoreDbContext _dbContext;
    private readonly IMapper _mapper;

    public GetUserForVerificationQueryHandler(ICoreDbContext dbContext, IMapper mapper)
    {
        _mapper = mapper;
        _dbContext = dbContext;
    }

    public async Task<ApiResponse<UsersVerificationVm>> Handle(GetUserForVerificationQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var query = _dbContext.Accounts.Where(x => x.ForVerification
                    && (x.AccountStatusId == AccountStatus.Migrated
                        || x.AccountStatusId == AccountStatus.Completed)
                    && (x.UserTypeId == UserTypes.Player)).OrderByDescending(m => m.CreatedOn).AsQueryable();

            if (request.BranchId.HasValue)
                query = query.Where(q => q.Branch.BranchId == request.BranchId.Value).AsQueryable();

            var total = await query.CountAsync();

            var finalQuery = GetPagedQueryUser(query, request);

            var userForVerificationList = await finalQuery
                .ProjectTo<UserVerificationDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            foreach (var item in userForVerificationList)
            {
                var upline = await _dbContext.Accounts.Where(m => m.RefferralKey == item.RefferralCode).FirstOrDefaultAsync();
                item.Recruiter = (upline != null) ? $"{upline.FirstName} {upline.LastName}" : "N/A";
                item.RecruiterAccountObjId = (upline != null) ? upline.AccountObjectId : null;
            }

            return new ApiResponse<UsersVerificationVm>()
            {
                Data = new UsersVerificationVm
                {
                    VerificationUsers = userForVerificationList,
                    Total = total,
                    PageNumber = (request.PagedQuery != null) ? request.PagedQuery.PageNumber : 1,
                    PageSize = (request.PagedQuery != null) ? request.PagedQuery.PageSize : userForVerificationList.Count()
                }
            };
        }
        catch (Exception ex)
        {
            return new ApiResponse<UsersVerificationVm>() { Success = false, ErrorMessage = ex.Message };
        }
    }

    public IQueryable<Account> GetPagedQueryUser(IQueryable<Account> query, GetUserForVerificationQuery request)
    {
        if (request.PagedQuery != null)
        {
            if (!string.IsNullOrEmpty(request.PagedQuery.Search))
                query = query.Where(q => q.FirstName.ToLower().Contains(request.PagedQuery.Search.ToLower())
                    || q.LastName.ToLower().Contains(request.PagedQuery.Search.ToLower())
                    || q.MobileNumber.Contains(request.PagedQuery.Search));
                
            if (request.PagedQuery.PageNumber > 0)
                query = query.Skip((request.PagedQuery.PageNumber) * request.PagedQuery.PageSize);

            query = query.Take(request.PagedQuery.PageSize);
        }

        if (request.DateFrom.HasValue && request.DateTo.HasValue)
            query = query.Where(q => q.CreatedOn >= request.DateFrom && q.CreatedOn <= request.DateTo);

        return query;
    }
}
