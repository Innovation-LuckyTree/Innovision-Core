using Innovision.Core.Application.Requests.GameCategories.Queries;
using Innovision.Core.Application.Requests.GameProviders.Queries;
using Innovision.Core.Application.Requests.Games.Commands.CreateGame;
using Innovision.Core.Application.Requests.Games.Commands.UpdateGame;
using Innovision.Core.Application.Requests.Games.Commands.UpdateGameMissedDraw;
using Innovision.Core.Application.Requests.Games.Queries.GetGames;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace Innovision.Core.API.Controllers;

public class GameController : ApiBaseController
{
    private readonly ILogger<GameController> _logger;
    private readonly IMemoryCache _memoryCache;

    public GameController(ILogger<GameController> logger, IMemoryCache memoryCache)
    {
        _logger = logger;
        _memoryCache = memoryCache;
    }

    [HttpGet]
    public async Task<ActionResult> Get(CancellationToken cancellationToken)
    {
        var games = await _memoryCache.GetOrCreateAsync(new { Controller = nameof(GameController), type = "games" }, async entry =>
        {
            entry.SetAbsoluteExpiration(TimeSpan.FromHours(8));

            var result = await Mediator.Send(new GetGamesQuery(), cancellationToken);
            return Ok(result);
        });

        return Ok(games?.Value);
    }

    [HttpPut]
    public async Task<ActionResult> Put(UpdateGameCommand command, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(command, cancellationToken);
        return Ok(result);
    }

    [HttpPatch("standard/missed-draw")]
    public async Task<ActionResult> UpdateGameStandardMissedDraw(UpdateGameMissedDrawCommand command, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(command, cancellationToken);
        return Ok(result);
    }

    ////New Version
    /// 
    [HttpGet("GameCategory")]
    public async Task<IActionResult> GetGameCategories(CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(new GetGameCategoriesQuery(), cancellationToken);

        return Ok(result);
    }
    
    [HttpGet("GameProvider")]
    public async Task<IActionResult> GetGameProviders(CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(new GetGameProviderListQuery(), cancellationToken);

        return Ok(result);
    }
    
    [HttpGet("GameCategory/GameProvider/{GameCategoryid}")]
    public async Task<IActionResult> GetGameProviderByCategoryId(int GameCategoryid, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(new GetGameProviderListByCategoryIdQuery(GameCategoryid), cancellationToken);

        return Ok(result);
    }
    
}

