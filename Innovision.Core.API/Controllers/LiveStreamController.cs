using Innovision.Core.Application.Requests.LiveStreams.Commands.CreateLiveStream;
using Innovision.Core.Application.Requests.LiveStreams.Queries.GetLatestLiveStream;
using Innovision.Core.Application.Requests.LiveStreams.Queries.GetLiveStreamList;
using Microsoft.AspNetCore.Mvc;

namespace Innovision.Core.API.Controllers;

public class LiveStreamController(ILogger<LiveStreamController> logger) : ApiBaseController
{
    private readonly ILogger<LiveStreamController> _logger = logger;

    [HttpPost]
    public async Task<ActionResult> CreateLiveStream(CreateLiveStreamCommand request, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(request, cancellationToken);
        return Ok(result);
    }

    [HttpGet("{branchId}/latest")]
    public async Task<ActionResult> GetLatestLiveStream(int branchId, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(new GetLatestLiveStreamQuery(branchId), cancellationToken);
        return Ok(result);
    }

    [HttpPost("search")]
    public async Task<ActionResult> GetLiveStreamList(GetLiveStreamListQuery request, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(request, cancellationToken);
        return Ok(result);
    }
}
