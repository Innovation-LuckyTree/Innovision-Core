using AutoMapper;
using AutoMapper.QueryableExtensions;
using Innovision.Core.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Innovision.Core.Application.Requests.Games.Queries;

public record GetGamesByGameProviderQuery(int GameProviderId) : IRequest<GameVm>;

public class GetGamesByGameProviderQueryHandler(ICoreDbContext dbContext, IMapper mapper) : IRequestHandler<GetGamesByGameProviderQuery, GameVm>
{
    private readonly ICoreDbContext _dbContext = dbContext;
    private readonly IMapper _mapper = mapper;

    public async Task<GameVm> Handle(GetGamesByGameProviderQuery request, CancellationToken cancellationToken)
    {
        var games = await _dbContext.Games
            .Where(x => x.GameProviderId == request.GameProviderId)
            .ProjectTo<GameDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        return new GameVm(games);
    }
}