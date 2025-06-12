using AutoMapper;
using AutoMapper.QueryableExtensions;
using Innovision.Core.Application.Common;
using Innovision.Core.Application.Exceptions;
using Innovision.Core.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Innovision.Core.Application.Requests.SelfExclusion.Queries.GetActiveExclusionById;

public class GetActiveExclusionByIdQueryHandler(ICoreDbContext dbContext, IMapper mapper) : IRequestHandler<GetActiveExclusionByIdQuery, SelfExclusionDto>
{
    private readonly ICoreDbContext _dbContext = dbContext;
    private readonly IMapper _mapper = mapper;
    public async Task<SelfExclusionDto> Handle(GetActiveExclusionByIdQuery request, CancellationToken cancellationToken)
    {
        var today = DateTime.UtcNow;
        var activeExclusion = await _dbContext.SelfExclusions.Where(m => m.AccountId == request.AccountId 
            && ((m.DateStart < today && m.DateEnd > today) || m.IsIndefinite) 
            && m.Status == 1)
        .OrderByDescending(m => m.SelfExclusionId)
        .ProjectTo<SelfExclusionDto>(_mapper.ConfigurationProvider)
        .FirstOrDefaultAsync(cancellationToken);

        _ = activeExclusion ?? throw new EntityNotFoundException("SelfExclusion", request.AccountId);

        return activeExclusion;

    }
}
