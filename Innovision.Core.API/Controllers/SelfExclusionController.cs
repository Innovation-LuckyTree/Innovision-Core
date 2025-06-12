using Innovision.Core.Application.Requests.SelfExclusion.Commands.CreateNewExclusion;
using Innovision.Core.Application.Requests.SelfExclusion.Commands.UpdateCurrentExclusion;
using Innovision.Core.Application.Requests.SelfExclusion.Queries.GetActiveExclusionById;
using Microsoft.AspNetCore.Mvc;

namespace Innovision.Core.API.Controllers;
public class SelfExclusionController : ApiBaseController
{
    private readonly ILogger<SelfExclusionController> _logger;

    public SelfExclusionController(ILogger<SelfExclusionController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    public async Task<ActionResult> GetActiveExclusion([FromQuery] long AccountId, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(new GetActiveExclusionByIdQuery(AccountId), cancellationToken);
        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult> CreateSelfExclusion([FromBody] CreateNewExclusionCommand request, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(request, cancellationToken);
        return Ok(result);
    }

    [HttpPatch]
    public async Task<ActionResult> UpdateSelfExclusion([FromBody] UpdateCurrentExclusionCommand request, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(request, cancellationToken);
        return Ok(result);
    }

}