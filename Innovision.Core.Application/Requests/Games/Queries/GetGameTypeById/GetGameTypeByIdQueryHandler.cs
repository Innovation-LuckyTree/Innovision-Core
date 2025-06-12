using AutoMapper;
using AutoMapper.QueryableExtensions;
using Innovision.Core.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Innovision.Core.Application.Requests.Games.Queries.GetGameTypeById;

public class GetGameTypeByIdQueryHandler : IRequestHandler<GetGameTypeByIdQuery, GameTypesDto>
{
    private readonly ICoreDbContext _dbContext;
    private readonly IMapper _mapper;

    public GetGameTypeByIdQueryHandler(ICoreDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<GameTypesDto> Handle(GetGameTypeByIdQuery request, CancellationToken cancellationToken)
        => await _dbContext.GameTypes.Where(o => o.GameTypeId == request.GameTypeId)
            .ProjectTo<GameTypesDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(cancellationToken);
}