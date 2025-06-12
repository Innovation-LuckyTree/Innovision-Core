using Innovision.Core.Application.Requests.Players.Queries.FindPlayer;
using Innovision.Core.Application.Requests.Players.Queries.GetBlockedUsers;
using Innovision.Core.Application.Requests.Players.Queries.GetCurrentPlayerAgentInfo;
using Innovision.Core.Application.Requests.Players.Queries.GetCurrentUser;
using Innovision.Core.Application.Requests.Players.Queries.GetInActivePlayers;
using Innovision.Core.Application.Requests.Players.Queries.GetLockedUsers;
using Innovision.Core.Application.Requests.Players.Queries.GetLockedUsersExport;
using Innovision.Core.Application.Requests.Players.Queries.GetOfflinePlayers;
using Innovision.Core.Application.Requests.Players.Queries.GetOfflinePlayersExport;
using Innovision.Core.Application.Requests.Players.Queries.GetOnlinePlayers;
using Innovision.Core.Application.Requests.Players.Queries.GetOnlinePlayersExport;
using Innovision.Core.Application.Requests.Players.Queries.GetPayingUser;
using Innovision.Core.Application.Requests.Players.Queries.GetPlayerByAccountId;
using Innovision.Core.Application.Requests.Players.Queries.GetPlayerByCompanyId;
using Innovision.Core.Application.Requests.Players.Queries.GetPlayerInformationList;
using Innovision.Core.Application.Requests.Players.Queries.GetPlayerMigrateAccount;
using Innovision.Core.Application.Requests.Players.Queries.GetPlayersMigrateRange;
using Innovision.Core.Application.Requests.Players.Queries.GetPlayerStatus;
using Innovision.Core.Application.Requests.Players.Queries.GetPlayersUnusedQuery;
using Innovision.Core.Application.Requests.Players.Queries.GetPlayingUsersByAccountId;
using Innovision.Core.Application.Requests.Players.Queries.GetSummary;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Innovision.Core.API.Controllers;

public class PlayerController : ApiBaseController
{
    private readonly ILogger<PlayerController> _logger;

    public PlayerController(ILogger<PlayerController> logger)
    {
        _logger = logger;
    }

    [HttpGet("current/summary")]
    public async Task<IActionResult> GetCurrentBetsSummary(CancellationToken cancellationToken)
    {
        var response = await Mediator.Send(new GetSummaryQuery(), cancellationToken);

        return Ok(response);
    }

    [HttpGet("current")]
    public async Task<IActionResult> GetCurrentUser(CancellationToken cancellationToken)
    {
        var response = await Mediator.Send(new GetCurrentUserQuery(), cancellationToken);

        return Ok(response);
    }

    [HttpGet("agent-info")]
    public async Task<IActionResult> GetPlayerAgent(CancellationToken cancellationToken)
    {
        var response = await Mediator.Send(new GetCurrentPlayerAgentInfoQuery(), cancellationToken);

        return Ok(response);
    }

    [HttpGet("account/{accountId}")]
    public async Task<IActionResult> GetPlayerByAccountId(long accountId, CancellationToken cancellationToken)
    {
        var response = await Mediator.Send(new GetPlayerByAccountIdQuery(accountId), cancellationToken);

        return Ok(response);
    }

    [HttpPost("account/list")]
    public async Task<IActionResult> GetPlayerAccountInformationList(GetPlayerInformationListQuery request, CancellationToken cancellationToken)
    {
        var response = await Mediator.Send(request, cancellationToken);

        return Ok(response);
    }

    [AllowAnonymous]
    [HttpPost("find")]
    public async Task<IActionResult> FindUser(FindPlayerQuery request, CancellationToken cancellationToken)
    {
        var response = await Mediator.Send(request, cancellationToken);

        return Ok(response);
    }

    [HttpPost("list")]
    public async Task<IActionResult> GetPlayerListByAccounts(GetPlayingUsersByAccountIdQuery request, CancellationToken cancellationToken)
    {
        var response = await Mediator.Send(request, cancellationToken);
        return Ok(response);
    }


    [HttpPost("playing/list")]
    public async Task<IActionResult> GetPlayingPlayers(GetPayingUserQuery request, CancellationToken cancellationToken)
    {
        var response = await Mediator.Send(request, cancellationToken);
        return Ok(response);
    }

    [HttpPost("locked/list")]
    public async Task<IActionResult> GetLockedPlayers(GetLockedUsersQuery request, CancellationToken cancellationToken)
    {
        var response = await Mediator.Send(request, cancellationToken);
        return Ok(response);
    }

    [HttpPost("locked/list/export")]
    public async Task<IActionResult> GetLockedPlayersExport(GetLockedUsersExportQuery request, CancellationToken cancellationToken)
    {
        var response = await Mediator.Send(request, cancellationToken);
        return Ok(response);
    }

    [HttpPost("blocked/list")]
    public async Task<IActionResult> GetBlockedPlayers(GetBlockedUsersQuery request, CancellationToken cancellationToken)
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

    [HttpPost("online/list/export")]
    public async Task<IActionResult> GetOnlinePlayersExport(GetOnlinePlayersExportQuery request, CancellationToken cancellationToken)
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

    [HttpPost("offline/list/export")]
    public async Task<IActionResult> GetOfflinePlayersExport(GetOfflinePlayersExportQuery request, CancellationToken cancellationToken)
    {
        var response = await Mediator.Send(request, cancellationToken);
        return Ok(response);
    }

    [HttpPost("inactive/list")]
    public async Task<IActionResult> GetInActivePlayers(GetInActivePlayersQuery request, CancellationToken cancellationToken)
    {
        var response = await Mediator.Send(request, cancellationToken);
        return Ok(response);
    }

    [HttpGet("company/list")]
    public async Task<IActionResult> GetPlayerByCompanyId([FromQuery] GetPlayerByCompanyIdQuery query, CancellationToken cancellationToken)
    {
        var response = await Mediator.Send(query, cancellationToken);
        return Ok(response);
    }

    [HttpPost("status")]
    public async Task<IActionResult> GetPlayerStatus(GetPlayerStatusQuery request, CancellationToken cancellationToken)
    {
        var response = await Mediator.Send(request, cancellationToken);
        return Ok(response);
    }

    [HttpGet("migrate/accounts/{accountObjectId}")]
    public async Task<IActionResult> GetPlayerMigrateAccountByAccountObjectId(Guid accountObjectId, CancellationToken cancellationToken)
    {
        var response = await Mediator.Send(new GetPlayerMigrateAccountQuery(accountObjectId), cancellationToken);
        return Ok(response);
    }

    [HttpPost("migrate/accounts/range")]
    public async Task<IActionResult> GetPlayersMigrateRange(GetPlayersMigrateRangeQuery request, CancellationToken cancellationToken)
    {
        var response = await Mediator.Send(request, cancellationToken);
        return Ok(response);
    }

    
    [HttpPost("unused/accounts")]
    public async Task<IActionResult> GetPlayersUnused(GetPlayersUnusedQuery request, CancellationToken cancellationToken)
    {
        var response = await Mediator.Send(request, cancellationToken);
        return Ok(response);
    }
}