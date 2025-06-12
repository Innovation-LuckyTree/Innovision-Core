using AutoMapper;
using AutoMapper.QueryableExtensions;
using Innovision.Core.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Innovision.Core.Application.Requests.Games.Queries.GetGameTypeList;

public class GetGameTypeListQueryHandler(ICoreDbContext dbContext, IMapper mapper) : IRequestHandler<GetGameTypeListQuery, GameTypesVm>
{
    private readonly ICoreDbContext _dbContext = dbContext;
    private readonly IMapper _mapper = mapper;

    public async Task<GameTypesVm> Handle(GetGameTypeListQuery request, CancellationToken cancellationToken)
    {
        var query = _dbContext.GameTypes.AsQueryable();

        if ((request?.GameTypes.Count() ?? 0) > 0)
        {
            query = query.Where(o => request.GameTypes.Contains(o.GameTypeId));
        }

        var result = await query.ProjectTo<GameTypesDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        return new GameTypesVm(result);
    }
}