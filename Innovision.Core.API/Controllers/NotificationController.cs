using Innovision.Core.Application.Requests.Notifications.Commands.CreateAccountsNotificationByName;
using Innovision.Core.Application.Requests.Notifications.Commands.CreateAccountsNotificationCommand;
using Innovision.Core.Application.Requests.Notifications.Commands.CreateBulkNotification;
using Innovision.Core.Application.Requests.Notifications.Commands.CreateBulkNotificationByName;
using Innovision.Core.Application.Requests.Notifications.Commands.CreateNotificationCommand;
using Innovision.Core.Application.Requests.Notifications.Commands.MarkAllNotificationsCommand;
using Innovision.Core.Application.Requests.Notifications.Commands.UpdateNotificationCommand;
using Innovision.Core.Application.Requests.Notifications.Queries.GetCredtiNotifcationById;
using Innovision.Core.Application.Requests.Notifications.Queries.GetNotificationsByAccountId;
using Microsoft.AspNetCore.Mvc;

namespace Innovision.Core.API.Controllers;

public class NotificationController(ILogger<NotificationController> logger) : ApiBaseController
{
  private readonly ILogger<NotificationController> _logger = logger;

    [HttpPost]
    public async Task<ActionResult> CreateNotification([FromBody] CreateNotificationCommand command, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(command, cancellationToken);
        return Ok(result);
    }

    [HttpPost("generate/account/list")]
    public async Task<ActionResult> CreateAccountsNotificationByName([FromBody] CreateAccountsNotificationByNameCommand command, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(command, cancellationToken);
        return Ok(result);
    }

    [HttpPost("generate/list")]
    public async Task<ActionResult> CreateBulkNotificationByName([FromBody] CreateBulkNotificationByNameCommand command, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(command, cancellationToken);
        return Ok(result);
    }


    [HttpPost("account/list")]
    public async Task<ActionResult> CreateAccountsNotification([FromBody] CreateAccountsNotificationCommand command, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(command, cancellationToken);
        return Ok(result);
    }

    [HttpPost("list")]
    public async Task<ActionResult> CreateBulkNotification([FromBody] CreateBulkNotificationCommand command, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(command, cancellationToken);
        return Ok(result);
    }

    [HttpPost("search")]
    public async Task<ActionResult> Search([FromBody] GetNotificationsByAccountIdQuery query, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(query, cancellationToken);
        return Ok(result);
    }

    [HttpPut]
    public async Task<ActionResult> Put(UpdateNotificationCommand command, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(command, cancellationToken);
        return Ok(result);
    }

    [HttpPut("mark-all")]
    public async Task<ActionResult> MarkAll(MarkAllNotificationsCommand command, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(command, cancellationToken);
        return Ok(result);
    }

    [HttpGet("credit/recipient")]
    public async Task<ActionResult> GetCreditNotificationRecipient([FromQuery] GetCredtiNotifcationByIdQuery request, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(request, cancellationToken);
        return Ok(result);
    }
}
