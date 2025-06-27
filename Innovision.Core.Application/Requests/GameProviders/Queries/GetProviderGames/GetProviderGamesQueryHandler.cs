
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Innovision.Core.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Innovision.Core.Application.Requests.GameProviders.Queries;

public class GetProviderGamesQueryHandler(ICoreDbContext dbContext, IMapper mapper) : IRequestHandler<GetProviderGamesQuery, GameProviderVm>
{
    private readonly ICoreDbContext _dbContext = dbContext;
    private readonly IMapper _mapper = mapper;

    public async Task<GameProviderVm> Handle(GetProviderGamesQuery request, CancellationToken cancellationToken)
    {
        var gameProviders = await _dbContext.GameCatalogs
            .Where(x => x.GameCatalogId == request.GameCategoryId)
            .Select(g => g.Game.GameProvider)
            .ProjectTo<GameProvidersDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        return new GameProviderVm(gameProviders);
    }
}