using AutoMapper;
using AutoMapper.QueryableExtensions;
using Innovision.Core.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Innovision.Core.Application.Requests.Games.Queries;

public record GetGamesByGameProviderAndCategoryQuery(int GameProviderId, int GameCategoryId) : IRequest<GameVm>;

public class GetGamesByGameProviderAndCategoryQueryHandler(ICoreDbContext dbContext, IMapper mapper) : IRequestHandler<GetGamesByGameProviderAndCategoryQuery, GameVm>
{
    private readonly ICoreDbContext _dbContext = dbContext;
    private readonly IMapper _mapper = mapper;

    public async Task<GameVm> Handle(GetGamesByGameProviderAndCategoryQuery request, CancellationToken cancellationToken)
    {
        var games = await _dbContext.GameCatalogs
            .Where(x => x.Game.GameProviderId == request.GameProviderId
                && x.GameCategoryId == request.GameCategoryId)
            .Select(g => g.Game)
            .ProjectTo<GameDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        return new GameVm(games);
    }
}