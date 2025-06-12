using AutoMapper;
using AutoMapper.QueryableExtensions;
using Innovision.Core.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Innovision.Core.Application.Requests.Faqs.Queries.GetFaqs;

public class SearchFaqsQueryHandler(ICoreDbContext dbContext, IMapper mapper) : IRequestHandler<SearchFaqsQuery, FaqVm>
{
    private readonly ICoreDbContext _dbContext = dbContext;
    private readonly IMapper _mapper = mapper;

    public async Task<FaqVm> Handle(SearchFaqsQuery request, CancellationToken cancellationToken)
    {
        var query = _dbContext.FrequentlyAskQuestions.Include(e => e.Game).AsQueryable();
        var totalCount = 0;

        if ((request?.GameId ?? 0) > 0)
            query = query.Where(o => o.GameId == request.GameId);

        if (request?.IsApplicationRelated ?? false)
            query = query.Where(o => o.IsApplicationRelated == 1);

        totalCount = await query.CountAsync(cancellationToken);

        if (request != null)
        {
            if (string.IsNullOrEmpty(request.PagedQuery.Search))
            {
                query = query.Where(o => o.Question.Contains(request.PagedQuery.Search) | o.Answer.Contains(request.PagedQuery.Search));
            }

            query = query.Skip(request.PagedQuery.PageSize * request.PagedQuery.PageNumber);
            query = query.Take(request.PagedQuery.PageSize);
        }

        var faqs = await query.ProjectTo<FaqDto>(_mapper.ConfigurationProvider).ToListAsync(cancellationToken);

        return new FaqVm(faqs);
    }
}