using AutoMapper;
using AutoMapper.QueryableExtensions;
using Innovision.Core.Application.Common;
using Innovision.Core.Application.Interfaces;
using Innovision.Core.Common.Models;
using Innovision.Core.Domain.Entity;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Innovision.Core.Application.Requests.JackpotWinners.Queries.GetJackpotWinnerList;

public class GetJackpotListQueryHandler(ICoreDbContext coreDbContext, IMapper mapper) : IRequestHandler<GetJackpotWinnerListQuery, ApiResponse<PaginateResult<JackpotWinnerInfo>>>
{
    private readonly ICoreDbContext _coreDbContext = coreDbContext;
    private readonly IMapper _mapper = mapper;

    public async Task<ApiResponse<PaginateResult<JackpotWinnerInfo>>> Handle(GetJackpotWinnerListQuery request, CancellationToken cancellationToken)
    {
        var query = _coreDbContext.JackpotWinners
            .Where(o => o.CompanyGameId == request.CompanyGameId);

        if (request.Request.JackpotStatusId != null)
            query = query.Where(o => o.CompanyGameId == request.CompanyGameId);

        query = query
            .OrderByDescending(x => x.DrawDate)
            .ThenByDescending(x => x.DrawTime).AsQueryable();

        var totalCount = query.Count();
        if (request.Request.PageQuery != null)
            query = FilterQuery(query, request.Request.PageQuery);


        var results = await query
            .ProjectTo<JackpotWinnerInfo>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        return new ApiResponse<PaginateResult<JackpotWinnerInfo>>()
        {
            Data = new PaginateResult<JackpotWinnerInfo>
            {
                PageSize = request.Request.PageQuery?.PageSize ?? totalCount,
                PageNumber = request.Request.PageQuery?.PageNumber ?? 1,
                Total = totalCount,
                ListData = results
            },
        };
    }

    public IQueryable<JackpotWinner> FilterQuery(IQueryable<JackpotWinner> query, PagedQuery pagedQuery)
    {
        if (!string.IsNullOrEmpty(pagedQuery.Search))
            query = query.Where(q => (q.Account.FirstName.ToLower() + " " + q.Account.LastName.ToLower()).Contains(pagedQuery.Search.ToLower())
              || q.TransactionNo.ToLower().Contains(pagedQuery.Search.ToLower()));

        if (pagedQuery.PageNumber > 0)
            query = query.Skip((pagedQuery.PageNumber) * pagedQuery.PageSize);

        query = query.Take(pagedQuery.PageSize);

        return query;
    }
}