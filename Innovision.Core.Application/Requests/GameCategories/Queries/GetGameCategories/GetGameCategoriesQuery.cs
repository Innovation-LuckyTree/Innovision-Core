using MediatR;

namespace Innovision.Core.Application.Requests.GameCategories.Queries;

public record GetGameCategoriesQuery : IRequest<GameCategoryVm>
{

}
