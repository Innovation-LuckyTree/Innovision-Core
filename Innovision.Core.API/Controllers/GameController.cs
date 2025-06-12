using Innovision.Core.Application.Requests.Games.Commands.AddGameType;
using Innovision.Core.Application.Requests.Games.Commands.CreateGame;
using Innovision.Core.Application.Requests.Games.Commands.UpdateGame;
using Innovision.Core.Application.Requests.Games.Commands.UpdateGameMissedDraw;
using Innovision.Core.Application.Requests.Games.Queries.GetGames;
using Innovision.Core.Application.Requests.Games.Queries.GetGameTypeById;
using Innovision.Core.Application.Requests.Games.Queries.GetGameTypeList;
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

    [HttpGet("gametype/list")]
    public async Task<ActionResult> GetGameTypeById([FromQuery]IEnumerable<int> ids, CancellationToken cancellationToken)
    {
        var gameType = await _memoryCache.GetOrCreateAsync(new { Controller = nameof(GameController), Type = "gameTypeList", Ids = ids }, async entry =>
        {
            entry.SetAbsoluteExpiration(TimeSpan.FromHours(8));
            var result = await Mediator.Send(new GetGameTypeListQuery(ids), cancellationToken);

            return Ok(result);
        });

        return Ok(gameType?.Value);
    }

    [HttpGet("gametype/{id}")]
    public async Task<ActionResult> GetGameTypeById(int id, CancellationToken cancellationToken)
    {
        var gameType = await _memoryCache.GetOrCreateAsync(new { Controller = nameof(GameController), Type = "gameType", Id = id }, async entry =>
        {
            entry.SetAbsoluteExpiration(TimeSpan.FromHours(8));
            var result = await Mediator.Send(new GetGameTypeByIdQuery(id), cancellationToken);

            return Ok(result);
        });

        return Ok(gameType?.Value);
    }

    [HttpPost]
    public async Task<ActionResult> Post(CreateGameCommand request, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(request, cancellationToken);
        return Ok(result);
    }

    [HttpPost("gametype")]
    public async Task<ActionResult> CreateGameType(AddGameTypeCommand request, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(request, cancellationToken);
        return Ok(result);
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

