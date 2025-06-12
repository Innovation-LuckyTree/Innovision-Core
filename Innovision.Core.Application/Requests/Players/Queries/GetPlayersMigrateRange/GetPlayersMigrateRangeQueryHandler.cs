using AutoMapper;
using AutoMapper.QueryableExtensions;
using Innovision.Core.Application.Common;
using Innovision.Core.Application.Common.Contants;
using Innovision.Core.Application.Interfaces;
using Innovision.Core.Common.Models;
using Innovision.Core.Domain.Entity;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Innovision.Core.Application.Requests.Players.Queries.GetPlayersMigrateRange;

public class GetPlayersMigrateRangeQueryHandler : IRequestHandler<GetPlayersMigrateRangeQuery, ApiResponse<GetPlayerMigrateRangeVM>>
{
    private readonly IMapper _mapper;
    private readonly ICoreDbContext _dbContext;

    public GetPlayersMigrateRangeQueryHandler(IMapper mapper, ICoreDbContext dbContext)
    {
        _mapper = mapper;
        _dbContext = dbContext;
    }

    public async Task<ApiResponse<GetPlayerMigrateRangeVM>> Handle(GetPlayersMigrateRangeQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var createdDateTo = request.CreatedDateTo.Date.AddDays(1).AddTicks(-1);
            var modifiedDateTo = request.ModifiedDateTo?.Date.AddDays(1).AddTicks(-1);

            var query = _dbContext.Accounts
                .Include(m => m.Branch)
                .Where(m => m.UserTypeId == UserContants.USER_TYPE_PLAYER
                    && ((m.CreatedOn >= request.CreatedDateFrom && m.CreatedOn <= createdDateTo)
                        || (m.LastModified >= request.ModifiedDateFrom && m.LastModified <= modifiedDateTo)))
                .OrderBy(x => x.AccountInfoId)
                .AsQueryable();

            if (request.PagedQuery != null)
                query = FilterQuery(query, request.PagedQuery);

            var userslist = await query
                .ProjectTo<PlayerMigrateAccountDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return new ApiResponse<GetPlayerMigrateRangeVM>()
            {
                Data = new GetPlayerMigrateRangeVM
                {
                    Players = userslist,
                    Total = userslist?.Count() ?? 0,
                    PageNumber = request.PagedQuery != null ? request.PagedQuery.PageNumber : 1,
                    PageSize = request.PagedQuery != null ? request.PagedQuery.PageSize : userslist.Count()
                }
            };
        }
        catch (Exception ex)
        {
            return new ApiResponse<GetPlayerMigrateRangeVM>() { Success = false, ErrorMessage = ex.Message };
        }
    }

    public IQueryable<Account> FilterQuery(IQueryable<Account> query, PagedQuery pagedQuery)
    {
        if (!string.IsNullOrEmpty(pagedQuery.Search))
        {
            query = query.Where(q => q.FirstName.ToLower().Contains(pagedQuery.Search.ToLower())
                || q.LastName.ToLower().Contains(pagedQuery.Search.ToLower())
                || q.MobileNumber.Contains(pagedQuery.Search));
        }

        if (pagedQuery.PageNumber > 0)
            query = query.Skip(pagedQuery.PageNumber * pagedQuery.PageSize);

        query = query.Take(pagedQuery.PageSize);

        return query;
    }
}