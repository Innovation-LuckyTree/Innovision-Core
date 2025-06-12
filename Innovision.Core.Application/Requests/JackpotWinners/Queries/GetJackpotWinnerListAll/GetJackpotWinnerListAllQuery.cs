using AutoMapper;
using AutoMapper.QueryableExtensions;
using Innovision.Core.Application.Common;
using Innovision.Core.Application.Interfaces;
using Innovision.Core.Application.Requests.JackpotWinners.Queries.GetJackpotWinnerList;
using Innovision.Core.Common.Models;
using Innovision.Core.Domain.Entity;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Innovision.Core.Application.Requests.JackpotWinners.Queries.GetJackpotWinnerListAll
{
    public class GetJackpotWinnerListAllQuery : IRequest<ApiResponse<PaginateResult<JackpotWinnerInfo>>>
    {
        public Guid? CompanyId { get; set; }
        public int? BranchId { get; set; }
        public  DateTimeOffset? DateFrom { get; set; }
        public  DateTimeOffset? DateTo { get; set; }
        public bool? downloadReport { get; set; }
        public PagedQuery? PagedQuery { get; set; }
    }

    public class GetJackpotWinnerListAllQueryHandler(ICoreDbContext coreDbContext, IMapper mapper) : IRequestHandler<GetJackpotWinnerListAllQuery, ApiResponse<PaginateResult<JackpotWinnerInfo>>>
    {
        private readonly ICoreDbContext _coreDbContext = coreDbContext;
        private readonly IMapper _mapper = mapper;

        public async Task<ApiResponse<PaginateResult<JackpotWinnerInfo>>> Handle(GetJackpotWinnerListAllQuery request, CancellationToken cancellationToken)
        {
            var query = _coreDbContext.JackpotWinners
                .Include(m => m.Account)
                    .ThenInclude(m => m.Branch)
                .AsQueryable();

            if (request.BranchId.HasValue)
                query = query.Where(o => o.Account.BranchId == request.BranchId.Value);

            var totalCount = query.Count();

            query = query .OrderByDescending(x => x.DrawDate)
                .ThenByDescending(x => x.DrawTime).AsQueryable();

            query = FilterQuery(query, request);

            var results = await query
                .ProjectTo<JackpotWinnerInfo>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return new ApiResponse<PaginateResult<JackpotWinnerInfo>>()
            {
                Data = new PaginateResult<JackpotWinnerInfo>
                {
                    PageSize = (request.PagedQuery != null) ? request.PagedQuery.PageSize : results.Count(),
                    PageNumber = (request.PagedQuery != null) ? request.PagedQuery.PageNumber : 1,
                    Total = totalCount,
                    ListData = results
                },
            };
        }

        public IQueryable<JackpotWinner> FilterQuery(IQueryable<JackpotWinner> query, GetJackpotWinnerListAllQuery request)
        {
            if (!string.IsNullOrEmpty(request.PagedQuery.Search))
                query = query.Where(q => (q.Account.FirstName.ToLower() + " " + q.Account.LastName.ToLower()).Contains(request.PagedQuery.Search.ToLower())
                  || q.TransactionNo.ToLower().Contains(request.PagedQuery.Search.ToLower()));

            if (!request.downloadReport.HasValue)
            {
                if (request.PagedQuery.PageNumber > 0)
                    query = query.Skip((request.PagedQuery.PageNumber) * request.PagedQuery.PageSize);

                query = query.Take(request.PagedQuery.PageSize);
            }

            return query;
        }
    }
}
