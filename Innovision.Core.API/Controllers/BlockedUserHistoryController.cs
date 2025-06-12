using Innovision.Core.Application.Requests.Administratives.Queries.GetBlockedUsersExport;
using Innovision.Core.Application.Requests.BlockedUserHistories.Commands.BlockUser;
using Innovision.Core.Application.Requests.BlockedUserHistories.Commands.UnblockUser;
using Innovision.Core.Application.Requests.BlockedUserHistories.Queries.GetBlockedUsers;
using Innovision.Core.Application.Requests.BlockedUserHistories.Queries.GetBlockHistoryById;
using Innovision.Core.Application.Requests.BlockedUserHistories.Queries.GetUsersBlockHistories;
using Microsoft.AspNetCore.Mvc;

namespace Innovision.Core.API.Controllers
{
  public class BlockedUserHistoryController : ApiBaseController
  {
    private readonly ILogger<BlockedUserHistoryController> _logger;

    public BlockedUserHistoryController(ILogger<BlockedUserHistoryController> logger)
    {
      _logger = logger;
    }

    [HttpPost("block")]
    public async Task<ActionResult> BlockUser(BlockUserCommand command, CancellationToken cancellationToken)
    {
      var result = await Mediator.Send(command, cancellationToken);
      return Ok(result);
    }

    [HttpPost("unblock")]
    public async Task<ActionResult> UnblockUser(UnblockUserCommand command, CancellationToken cancellationToken)
    {
      var result = await Mediator.Send(command, cancellationToken);
      return Ok(result);
    }

    [HttpPost("active/list")]
    public async Task<ActionResult> GetBlockedUsers([FromBody] GetBlockedUsersListQuery query, CancellationToken cancellationToken)
    {
      var result = await Mediator.Send(query, cancellationToken);
      return Ok(result);
    }

    [HttpPost("active/list/export")]
    public async Task<ActionResult> GetBlockedUsersExport([FromBody] GetBlockedUsersExportQuery query, CancellationToken cancellationToken)
    {
      var result = await Mediator.Send(query, cancellationToken);
      return Ok(result);
    }

    [HttpGet("{blockedUserHistoryId}")]
    public async Task<ActionResult> GetBlockUserHistoryById(int blockedUserHistoryId, CancellationToken cancellationToken)
    {
      var result = await Mediator.Send(new GetBlockHistoryByIdQuery(blockedUserHistoryId), cancellationToken);
      return Ok(result);
    }

    [HttpGet("{accountInfoId}/history")]
    public async Task<ActionResult> GetUserBlockHistory(int accountInfoId, CancellationToken cancellationToken)
    {
      var result = await Mediator.Send(new GetUsersBlockHistoriesQuery(accountInfoId), cancellationToken);
      return Ok(result);
    }
  }
}
