using Innovision.Core.Application.Requests.Games.Commands.UpdateGame;
using Innovision.Core.Application.Requests.Games.Commands.UpdateGameMissedDraw;
using Innovision.Core.Application.Requests.Games.Queries.GetGames;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace Innovision.Core.API.Controllers;

public class GameController(ILogger<GameController> logger, IMemoryCache memoryCache) : ApiBaseController
{
    private readonly ILogger<GameController> _logger = logger;
    private readonly IMemoryCache _memoryCache = memoryCache;

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
}

