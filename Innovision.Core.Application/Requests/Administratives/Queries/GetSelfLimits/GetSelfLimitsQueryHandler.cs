using AutoMapper;
using AutoMapper.QueryableExtensions;
using Innovision.Core.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Innovision.Core.Application.Requests.ApplicationVersions.Queries.GetSelfLimits;

public class GetSelfLimitsQueryHandler(ICoreDbContext coreDbContext, IMapper mapper) : IRequestHandler<GetSelfLimitsQuery, SelfLimitVm>
{
    private readonly ICoreDbContext _coreDbContext = coreDbContext;
    private readonly IMapper _mapper = mapper;

    public async Task<SelfLimitVm> Handle(GetSelfLimitsQuery request, CancellationToken cancellationToken)
    {
        var query = _coreDbContext.SelfLimits
            .Include(m => m.Account)
                .ThenInclude(m => m.Branch)
            .Where(o => o.Status == request.Status)
            .OrderByDescending(m => m.CreatedOn)
            .AsQueryable();

        var total = await query.CountAsync();

        if (!string.IsNullOrEmpty(request.PagedQuery.Search))
        {
            query = query.Where(q => q.Account.FirstName.ToLower().Contains(request.PagedQuery.Search.ToLower())
                || q.Account.LastName.ToLower().Contains(request.PagedQuery.Search.ToLower())
                || q.Account.MobileNumber.Contains(request.PagedQuery.Search));
        }

        if (request.PagedQuery.PageSize > 0) { // check if pagination is bypassed (PageSize <= 0), for exports
            if (request.PagedQuery.SkipCount > 0)
                query = query.Skip(request.PagedQuery.SkipCount);

            query = query.Take(request.PagedQuery.PageSize);
        }

        var result = await query.ProjectTo<SelfLimitDto>(_mapper.ConfigurationProvider).ToListAsync(cancellationToken);

        return new SelfLimitVm(result)
        {
            Total = total,
            PageNumber = request.PagedQuery != null ? request.PagedQuery.PageNumber : 1,
            PageSize = request.PagedQuery != null ? request.PagedQuery.PageSize : result.Count()
        };

    }
}