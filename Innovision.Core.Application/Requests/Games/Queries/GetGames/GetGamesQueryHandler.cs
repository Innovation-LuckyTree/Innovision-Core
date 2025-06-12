using AutoMapper;
using AutoMapper.QueryableExtensions;
using Innovision.Core.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Innovision.Core.Application.Requests.Games.Queries.GetGames;

public class GetGamesQueryHandler : IRequestHandler<GetGamesQuery, GameVm>
{
    private readonly ICoreDbContext _dbContext;
    private readonly IMapper _mapper;

    public GetGamesQueryHandler(ICoreDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<GameVm> Handle(GetGamesQuery request, CancellationToken cancellationToken)
    {
        var games = await _dbContext.Games.ProjectTo<GameDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        return new GameVm(games);
    }
}