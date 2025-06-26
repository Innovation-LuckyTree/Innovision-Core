using AutoMapper;
using AutoMapper.QueryableExtensions;
using Innovision.Core.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Innovision.Core.Application.Requests.GameCategories.Queries;

public class GetGameCategoriesQueryHandler(ICoreDbContext dbContext, IMapper mapper) : IRequestHandler<GetGameCategoriesQuery, GameCategoryVm>
{
    private readonly ICoreDbContext _dbContext = dbContext;
    private readonly IMapper _mapper = mapper;

    public async Task<GameCategoryVm> Handle(GetGameCategoriesQuery request, CancellationToken cancellationToken)
    {
        var gameCategories = await _dbContext.GameProviders.ProjectTo<GameCategoryDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        return new GameCategoryVm(gameCategories);
    }
}