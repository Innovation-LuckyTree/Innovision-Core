using AutoMapper;
using AutoMapper.QueryableExtensions;
using Innovision.Core.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Innovision.Core.Application.Requests.GameProviders.Queries;
public record GetGameProviderListQuery : IRequest<GameProviderVm>
{

}

public class GetGameProviderListQueryHandler : IRequestHandler<GetGameProviderListQuery, GameProviderVm>
{
    private readonly ICoreDbContext _dbContext;
    private readonly IMapper _mapper;
    public async Task<GameProviderVm> Handle(GetGameProviderListQuery request, CancellationToken cancellationToken)
    {
        var gameProviders = await _dbContext.GameProvider.ProjectTo<GameProvidersDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        return new GameProviderVm(gameProviders);
    }
}