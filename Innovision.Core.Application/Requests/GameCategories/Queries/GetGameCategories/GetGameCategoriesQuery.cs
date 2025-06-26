using AutoMapper;
using AutoMapper.QueryableExtensions;
using Innovision.Core.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Innovision.Core.Application.Requests.GameCategories.Queries;

public record GetGameCategoriesQuery : IRequest<GameCategoryVm>
{

}

public class GetGameCategoriesQueryHandler : IRequestHandler<GetGameCategoriesQuery, GameCategoryVm>
{
    private readonly ICoreDbContext _dbContext;
    private readonly IMapper _mapper;
    public async Task<GameCategoryVm> Handle(GetGameCategoriesQuery request, CancellationToken cancellationToken)
    {
        var gameCategories = await _dbContext.GameProvider.ProjectTo<GameCategoryDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        return new GameCategoryVm(gameCategories);
    }
}