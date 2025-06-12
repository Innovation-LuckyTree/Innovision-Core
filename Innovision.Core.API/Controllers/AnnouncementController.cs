using Innovision.Core.Application.Requests.Announcements.Commands.CreateAnnouncementCommand;
using Innovision.Core.Application.Requests.Announcements.Commands.UpdateAnnouncementCommand;
using Innovision.Core.Application.Requests.Announcements.Queries.GetPaginatedAnnouncementsQuery;
using Innovision.Core.Application.Requests.Announcements.Queries.GetPendingAnnouncementsQuery;
using Innovision.Core.Application.Requests.Announcements.Queries.GetForNotificationListQuery;
using Microsoft.AspNetCore.Mvc;

namespace Innovision.Core.API.Controllers;

public class AnnouncementController(ILogger<AnnouncementController> logger) : ApiBaseController
{
  private readonly ILogger<AnnouncementController> _logger = logger;

  [HttpPost]
  public async Task<ActionResult> CreateAnnouncement([FromBody] CreateAnnouncementCommand command, CancellationToken cancellationToken)
  {
    var result = await Mediator.Send(command, cancellationToken);
    return Ok(result);
  }

  [HttpPost("search")]
  public async Task<ActionResult> Search([FromBody] GetPaginatedAnnouncementsQuery query, CancellationToken cancellationToken)
  {
    var result = await Mediator.Send(query, cancellationToken);
    return Ok(result);
  }

  [HttpPost("active")]
  public async Task<ActionResult> GetPendingAnnouncements(GetPendingAnnouncementsQuery request, CancellationToken cancellationToken)
  {
    var result = await Mediator.Send(request, cancellationToken);
    return Ok(result);
  }

  [HttpPut]
  public async Task<ActionResult> Put(UpdateAnnouncementCommand command, CancellationToken cancellationToken)
  {
    var result = await Mediator.Send(command, cancellationToken);
    return Ok(result);
  }

  [HttpGet("notifications")]
  public async Task<ActionResult> GetAnnouncementsForNotification(CancellationToken cancellationToken)
  {
    var result = await Mediator.Send(new GetForNotificationListQuery(), cancellationToken);
    return Ok(result);
  }
}
