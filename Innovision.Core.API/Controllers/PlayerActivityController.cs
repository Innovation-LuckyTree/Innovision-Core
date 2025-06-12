using Innovision.Core.Application.Requests.PlayerActivities.Commands.CreatePlayerActivity;
using Innovision.Core.Application.Requests.PlayerActivities.Commands.ProcessInactivePlayerActivity;
using Innovision.Core.Application.Requests.PlayerActivities.Commands.UpdateExtendAvtivity;
using Innovision.Core.Application.Requests.PlayerActivities.Queries.GetClosedDrawInactivePlayers;
using Innovision.Core.Application.Requests.PlayerActivities.Queries.GetExtendedPlayers;
using Innovision.Core.Application.Requests.PlayerActivities.Queries.GetExtendedPlayersExport;
using Innovision.Core.Application.Requests.PlayerActivities.Queries.GetInactivePlayers;
using Innovision.Core.Application.Requests.PlayerActivities.Queries.GetInactivePlayersExport;
using Innovision.Core.Application.Requests.PlayerActivities.Queries.GetLastDrawDateTime;
using Innovision.Core.Application.Requests.PlayerActivities.Queries.GetPlayerActivityByAccountId;
using Innovision.Core.Application.Requests.PlayerActivities.Queries.GetRequiredToPayPlayers;
using Innovision.Core.Application.Requests.PlayerActivities.Queries.GetRequiredToPlayPlayersExport;
using Microsoft.AspNetCore.Mvc;

namespace Innovision.Core.API.Controllers
{
    public class PlayerActivityController : ApiBaseController
    {
        private readonly ILogger<PlayerActivityController> _logger;

        public PlayerActivityController(ILogger<PlayerActivityController> logger)
        {
            _logger = logger;
        }

        [HttpPost("inactive/list")]
        public async Task<ActionResult> GetInactivePlayers(GetInactivePlayersQuery command, CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(command, cancellationToken);
            return (result.Success) ? Ok(result) : BadRequest(result);
        }

        [HttpPost("inactive/list/export")]
        public async Task<ActionResult> GetInactivePlayersExport(GetInactivePlayersExportQuery request, CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(request, cancellationToken);
            return Ok(result);
        }

        [HttpPost("extended/list")]
        public async Task<ActionResult> GetExtendedPlayers(GetExtendedPlayersQuery command, CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(command, cancellationToken);
            return (result.Success) ? Ok(result) : BadRequest(result);
        }

        [HttpPost("extended/list/export")]
        public async Task<ActionResult> GetExtendedPlayersExport(GetExtendedPlayersExportQuery request, CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(request, cancellationToken);
            return Ok(result);
        }

        [HttpPost("required/topay/list")]
        public async Task<ActionResult> GetRequiredToPayPlayers(GetRequiredToPayPlayersQuery command, CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(command, cancellationToken);
            return (result.Success) ? Ok(result) : BadRequest(result);
        }

        [HttpPost("required/toplay/list/export")]
        public async Task<ActionResult> GetRequiredToPlayPlayersExport(GetRequiredToPlayPlayersExportQuery request, CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(request, cancellationToken);
            return Ok(result);
        }

        [HttpPost("extend/draw")]
        public async Task<ActionResult> CreateUpdateForExtendPlayer(UpdateExtendAvtivityCommand command, CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(command, cancellationToken);
            return (result.Success) ? Ok(result) : BadRequest(result);
        }

        [HttpGet("latestdrawdatetime")]
        public async Task<IActionResult> GetLatestDrawDateTime(CancellationToken cancellationToken)
        {
            var response = await Mediator.Send(new GetLastDrawDateTimeQuery(), cancellationToken);

            return Ok(response);
        }

        [HttpPost("process/active")]
        public async Task<ActionResult> CreatePlayerActivity(CreatePlayerActivityCommand command, CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(command, cancellationToken);
            return Ok(result);
        }

        [HttpPost("closeddraw/inactive/list")]
        public async Task<ActionResult> GetClosedDrawInactivePlayers([FromBody] GetClosedDrawInactivePlayersQuery query, CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(query, cancellationToken);
            return Ok(result);
        }

        [HttpPost("process/inactive")]
        public async Task<ActionResult> ProcessPlayerInactivity(ProcessInactivePlayerActivityCommand command, CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(command, cancellationToken);
            return Ok(result);
        }

        [HttpGet("{accountId}")]
        public async Task<ActionResult> GetPlayerActivityByAccountId(int accountId, CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(new GetPlayerActivityByAccountIdQuery(accountId), cancellationToken);
            return result.Success ? Ok(result) : BadRequest(result);
        }
    }
}
