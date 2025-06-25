
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Innovision.Core.Application.Interfaces;
using Innovision.Core.Domain.Entity;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Innovision.Core.Application.Requests.GameProviders.Queries;

public record GetGameProviderListByCategoryIdQuery(int GameCategoryId) : IRequest<GameProviderVm>;

public class GetGameProviderListByCategoryIdQueryHandler : IRequestHandler<GetGameProviderListByCategoryIdQuery, GameProviderVm>
{
    private readonly ICoreDbContext _dbContext;
    private readonly IMapper _mapper;
    public async Task<GameProviderVm> Handle(GetGameProviderListByCategoryIdQuery request, CancellationToken cancellationToken)
    {
        var gameProviders = await _dbContext.Games
            .Where(x => x.GameCategoryId == request.GameCategoryId)
            .Select(g => g.GameProvider)
            .ProjectTo<GameProvidersDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        return new GameProviderVm(gameProviders);
    }
}