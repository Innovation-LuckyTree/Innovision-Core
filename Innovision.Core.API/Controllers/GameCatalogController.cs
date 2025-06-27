using Innovision.Core.Application.Requests.GameCategories.Queries;
using Innovision.Core.Application.Requests.GameProviders.Queries;
using Innovision.Core.Application.Requests.Games.Queries;
using Innovision.Core.Common.Models;
using Microsoft.AspNetCore.Mvc;

namespace Innovision.Core.API.Controllers;

public class GameCatalogController : ApiBaseController
{
    [HttpGet("categories")]
    public async Task<IActionResult> GetGameCategories(CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(new GetGameCategoriesQuery(), cancellationToken);

        return Ok(result);
    }

    [HttpGet("{categoryId}/providers")]
    public async Task<IActionResult> GetGameProviders(int categoryId, [FromForm] bool isFavorite, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(new GetGameProviderListQuery(categoryId, isFavorite), cancellationToken);

        return Ok(result);
    }

    [HttpGet("games/{gameCategoryId}/{providerId}")]
    public async Task<IActionResult> GetGameProviderByCategoryId(int gameCategoryId, int providerId, [FromQuery] PagedQuery? query, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(new GetGamesByProviderAndCategoryQuery(gameCategoryId, providerId, query), cancellationToken);

        return Ok(result);
    }
}