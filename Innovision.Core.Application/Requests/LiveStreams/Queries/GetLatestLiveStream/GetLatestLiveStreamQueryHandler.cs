using AutoMapper;
using AutoMapper.QueryableExtensions;
using Innovision.Core.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Innovision.Core.Application.Requests.LiveStreams.Queries.GetLatestLiveStream;

public class GetLatestLiveStreamQueryHandler(ICoreDbContext coreDbContext, IMapper mapper) : IRequestHandler<GetLatestLiveStreamQuery, LiveStreamDto>
{
    private readonly ICoreDbContext _coreDbContext = coreDbContext;
    private readonly IMapper _mapper = mapper;
    
    public async Task<LiveStreamDto> Handle(GetLatestLiveStreamQuery request, CancellationToken cancellationToken)
    {
        var result = await _coreDbContext.LiveStreams
            .Where(m => m.GameId == request.GameId)
            .OrderByDescending(o => o.LiveStreamId)
            .ProjectTo<LiveStreamDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(cancellationToken);
            
        return result;
    }
}