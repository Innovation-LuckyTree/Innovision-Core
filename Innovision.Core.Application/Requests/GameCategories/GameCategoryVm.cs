using Innovision.Core.Application.Requests.GameCategories.Queries;

namespace Innovision.Core.Application.Requests.GameCategories;


public record GameCategoryVm(IEnumerable<GameCategoryDto> GameCategories)
{
    public int Count
    {
        get
        {
            return GameCategories.Count();
        }
    }
}