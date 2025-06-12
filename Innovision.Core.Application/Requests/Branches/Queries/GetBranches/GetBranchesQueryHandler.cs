using AutoMapper;
using AutoMapper.QueryableExtensions;
using Innovision.Core.Application.Common;
using Innovision.Core.Application.Interfaces;
using Innovision.Core.Common.Models;
using Innovision.Core.Domain.Entity;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Innovision.Core.Application.Requests.Branches.Queries.GetBranches;

public class GetBranchesQueryHandler(IMapper mapper, ICoreDbContext dbContext) : IRequestHandler<GetBranchesQuery, ApiResponse<BranchListVm>>
{
    private readonly IMapper _mapper = mapper;
    private readonly ICoreDbContext _dbContext = dbContext;

    public async Task<ApiResponse<BranchListVm>> Handle(GetBranchesQuery request, CancellationToken cancellationToken)
    {
        var branchQuery = _dbContext.Branches
            .Include(o => o.Account)
            .Where(m => m.BranchId != -1 && !m.IsMain).AsQueryable();

        var totalCount = branchQuery.Count();

        if (request.PagedQuery != null)
            branchQuery = GetPagedQueryBranch(branchQuery, request.PagedQuery);

        var branches = await branchQuery
            .ProjectTo<BranchDto>(_mapper.ConfigurationProvider)
            .OrderBy(x => x.BranchId)
            .ToListAsync(cancellationToken);

        return new ApiResponse<BranchListVm>()
        {
            Data = new BranchListVm
            {
                BranchList = branches,
                Total = totalCount,
                PageNumber = (request.PagedQuery != null) ? request.PagedQuery.PageNumber : 1,
                PageSize = (request.PagedQuery != null) ? request.PagedQuery.PageSize : branches.Count(),
            }
        };
    }

    public IQueryable<Branch> GetPagedQueryBranch(IQueryable<Branch> branchQuery, PagedQuery pagedQuery)
    {
        if (!string.IsNullOrEmpty(pagedQuery.Search))
        {
            branchQuery = branchQuery.Where(q => q.BranchName.ToLower().Contains(pagedQuery.Search.ToLower()));
        }

        if (pagedQuery.PageNumber > 0)
        {
            branchQuery = branchQuery.Skip((pagedQuery.PageNumber) * pagedQuery.PageSize);
        }

        branchQuery = branchQuery.Take(pagedQuery.PageSize);

        return branchQuery;
    }
}