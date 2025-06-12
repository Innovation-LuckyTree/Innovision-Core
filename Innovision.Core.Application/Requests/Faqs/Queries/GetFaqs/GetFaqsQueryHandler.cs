using AutoMapper;
using AutoMapper.QueryableExtensions;
using Innovision.Core.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Innovision.Core.Application.Requests.Faqs.Queries.GetFaqs;

public class GetFaqsQueryHandler(ICoreDbContext dbContext, IMapper mapper) : IRequestHandler<GetFaqsQuery, FaqVm>
{
    private readonly ICoreDbContext _dbContext = dbContext;
    private readonly IMapper _mapper = mapper;

    public async Task<FaqVm> Handle(GetFaqsQuery request, CancellationToken cancellationToken)
    {
        var faqs = await _dbContext.FrequentlyAskQuestions
            .Include(e => e.Game)
            .ProjectTo<FaqDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        return new FaqVm(faqs);
    }
}