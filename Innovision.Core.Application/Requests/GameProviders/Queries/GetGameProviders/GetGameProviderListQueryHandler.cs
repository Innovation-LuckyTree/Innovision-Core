using AutoMapper;
using AutoMapper.QueryableExtensions;
using Innovision.Core.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Innovision.Core.Application.Requests.GameProviders.Queries;

public class GetGameProviderListQueryHandler(ICoreDbContext dbContext, IMapper mapper) : IRequestHandler<GetGameProviderListQuery, GameProviderVm>
{
    private readonly ICoreDbContext _dbContext = dbContext;
    private readonly IMapper _mapper = mapper;

    public async Task<GameProviderVm> Handle(GetGameProviderListQuery request, CancellationToken cancellationToken)
    {
        var gameProviders = await _dbContext.GameProviders.ProjectTo<GameProvidersDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        return new GameProviderVm(gameProviders);
    }
}