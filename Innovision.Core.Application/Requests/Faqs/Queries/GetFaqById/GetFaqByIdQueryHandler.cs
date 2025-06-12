using AutoMapper;
using AutoMapper.QueryableExtensions;
using Innovision.Core.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Innovision.Core.Application.Requests.Faqs.Queries.GetFaqById;

public class GetFaqByIdQueryHandler(ICoreDbContext dbContext, IMapper mapper) : IRequestHandler<GetFaqByIdQuery, FaqDto>
{
    private readonly ICoreDbContext _dbContext = dbContext;
    private readonly IMapper _mapper = mapper;
    
    public async Task<FaqDto> Handle(GetFaqByIdQuery request, CancellationToken cancellationToken)
    {
        return await _dbContext.FrequentlyAskQuestions
            .Include(e => e.Game)
            .Where(o => o.FrequentlyAskQuestionId == request.FrequentlyAskQuestionId)
            .ProjectTo<FaqDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(cancellationToken);
    }
}