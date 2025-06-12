using Innovision.Core.Application.Requests.Players.Queries.GetOfflinePlayers;
using Innovision.Core.Application.Requests.Players.Queries.GetOnlinePlayers;
using Innovision.Core.Application.Requests.Players.Queries.GetPayingUser;
using Microsoft.AspNetCore.Mvc;

namespace Innovision.Core.API.Controllers;

public class UserStatusController(ILogger<UserController> logger) : ApiBaseController
{
    private readonly ILogger<UserController> _logger = logger;

    [HttpPost("playing/list")]
    public async Task<IActionResult> GetPlayingPlayers(GetPayingUserQuery request, CancellationToken cancellationToken)
    {
        var response = await Mediator.Send(request, cancellationToken);
        return Ok(response);
    }

    [HttpPost("online/list")]
    public async Task<IActionResult> GetOnlinePlayers(GetOnlinePlayersQuery request, CancellationToken cancellationToken)
    {
        var response = await Mediator.Send(request, cancellationToken);
        return Ok(response);
    }

    [HttpPost("offline/list")]
    public async Task<IActionResult> GetOfflinePlayers(GetOfflinePlayersQuery request, CancellationToken cancellationToken)
    {
        var response = await Mediator.Send(request, cancellationToken);
        return Ok(response);
    }
}
