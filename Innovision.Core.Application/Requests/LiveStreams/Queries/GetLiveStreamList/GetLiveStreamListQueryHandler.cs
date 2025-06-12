using AutoMapper;
using AutoMapper.QueryableExtensions;
using Innovision.Core.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Innovision.Core.Application.Requests.LiveStreams.Queries.GetLiveStreamList;

public class GetLiveStreamListQueryHandler(ICoreDbContext coreDbContext, IMapper mapper) : IRequestHandler<GetLiveStreamListQuery, LiveStreamVm>
{
    private readonly ICoreDbContext _coreDbContext = coreDbContext;
    private readonly IMapper _mapper = mapper;

    public async Task<LiveStreamVm> Handle(GetLiveStreamListQuery request, CancellationToken cancellationToken)
    {
        var query = _coreDbContext.LiveStreams
            .OrderByDescending(o => o.LiveStreamId)
            .AsQueryable();

        var totalCount = await query.CountAsync(cancellationToken);

        if (!string.IsNullOrEmpty(request.PagedQuery?.Search))
            query = query.Where(o => o.Title.Contains(request.PagedQuery.Search));

        if ((request.PagedQuery?.PageNumber ?? 0) > 0)
            query = query.Skip(request.PagedQuery.SkipCount);

        query = query.Take(request.PagedQuery.PageSize);

        var result = await query.ProjectTo<LiveStreamDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        var response = new LiveStreamVm(result)
        {
            Offset = request.PagedQuery?.SkipCount ?? 0,
            PageSize = request.PagedQuery?.PageSize ?? 0,
            TotalCount = totalCount
        };

        return response;
    }
}