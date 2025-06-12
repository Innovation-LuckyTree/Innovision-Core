using AutoMapper;
using AutoMapper.QueryableExtensions;
using Innovision.Core.Application.Common;
using Innovision.Core.Application.Interfaces;
using Innovision.Core.Common.Models;
using Innovision.Core.Domain.Entity;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Innovision.Core.Application.Requests.PlayerActivities.Queries.GetExtendedPlayers
{
    public record GetExtendedPlayersQuery(PagedQuery? PagedQuery) : IRequest<ApiResponse<InactivePlayerVm>>;
    public class GetExtendedPlayersQueryHandler(ICoreDbContext dbContext, IMapper mapper) : IRequestHandler<GetExtendedPlayersQuery, ApiResponse<InactivePlayerVm>>
    {
        private readonly ICoreDbContext _dbContext = dbContext;
        private readonly IMapper _mapper = mapper;
        public async Task<ApiResponse<InactivePlayerVm>> Handle(GetExtendedPlayersQuery request, CancellationToken cancellationToken)
        {
            var query = _dbContext.PlayerActivities
               .Include(m => m.Account)
                   .ThenInclude(m => m.Branch)
               .Where(m => m.Extended > 0)
               .AsQueryable();

            var totalCount = query.Count();
            if (request.PagedQuery != null)
                query = FilterQuery(query, request.PagedQuery);

            var resultData = await query
                    .ProjectTo<InactivePlayerDto>(_mapper.ConfigurationProvider)
                    .ToListAsync(cancellationToken);

            //var standardMissedDraws = await _dbContext.Companies.Where(m => m.CompanyId == request.CompanyId).Select(m => m.StandardMissedDraw).FirstOrDefaultAsync(cancellationToken);
            //foreach (var item in resultData)
            //{
            //    item.StandardMissedDraw = standardMissedDraws;
            //}

            return new ApiResponse<InactivePlayerVm>()
            {
                Data = new InactivePlayerVm
                {
                    Results = resultData,
                    Total = totalCount,
                    PageNumber = (request.PagedQuery != null) ? request.PagedQuery.PageNumber : 1,
                    PageSize = (request.PagedQuery != null) ? request.PagedQuery.PageSize : resultData.Count()
                }
            };
        }

        public IQueryable<PlayerActivity> FilterQuery(IQueryable<PlayerActivity> query, PagedQuery pagedQuery)
        {
            if (!string.IsNullOrEmpty(pagedQuery.Search))
                query = query.Where(q => (q.Account.FirstName.ToLower() + " " + q.Account.LastName.ToLower()).Contains(pagedQuery.Search.ToLower()));

            if (pagedQuery.PageNumber > 0)
                query = query.Skip((pagedQuery.PageNumber) * pagedQuery.PageSize);

            query = query.Take(pagedQuery.PageSize);

            return query;
        }
    }
}
