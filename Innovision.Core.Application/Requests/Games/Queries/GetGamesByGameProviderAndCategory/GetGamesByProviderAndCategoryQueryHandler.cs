using AutoMapper;
using AutoMapper.QueryableExtensions;
using Innovision.Core.Application.Interfaces;
using Innovision.Core.Common.Models;
using Innovision.Core.Domain.Entity;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Innovision.Core.Application.Requests.Games.Queries;

public class GetGamesByProviderAndCategoryQueryHandler(ICoreDbContext dbContext, IMapper mapper) : IRequestHandler<GetGamesByProviderAndCategoryQuery, GameVm>
{
    private readonly ICoreDbContext _dbContext = dbContext;
    private readonly IMapper _mapper = mapper;

    public async Task<GameVm> Handle(GetGamesByProviderAndCategoryQuery request, CancellationToken cancellationToken)
    {
        var gamesQuery = _dbContext.Games.AsQueryable();

        if (request.GameCategoryId > 0)
        {
            gamesQuery = gamesQuery.Where(x => x.GameCatalogs.Any(c => c.GameCategoryId == request.GameCategoryId));
        }

        if (request.GameProviderId > 0)
        {
            gamesQuery = gamesQuery.Where(x => x.GameProviderId == request.GameProviderId);
        }

        var games = await GetGamesByProviderAndCategory(gamesQuery, request.Query ?? new PagedQuery())
            .ProjectTo<GameDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        return new GameVm(games);
    }

    private IQueryable<Game> GetGamesByProviderAndCategory(IQueryable<Game> query, PagedQuery pagedQuery)
    {
        if (pagedQuery.SkipCount > 0)
        {
            query = query.Skip(pagedQuery.SkipCount);
        }

        query = query.Take(pagedQuery.PageSize);

        return query;
    }
}