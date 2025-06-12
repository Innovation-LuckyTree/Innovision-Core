using Innovision.Core.Application.Requests.QuarantineKafkas.Commands.CreateQuarantine;
using Innovision.Core.Application.Requests.QuarantineKafkas.Commands.UpdateQuarantine;
using Innovision.Core.Application.Requests.QuarantineKafkas.Queries.GetActiveQuarantines;
using Microsoft.AspNetCore.Mvc;

namespace Innovision.Core.API.Controllers;

public class QuarantineKafkaController : ApiBaseController
{
    private readonly ILogger<QuarantineKafkaController> _logger;

    public QuarantineKafkaController(ILogger<QuarantineKafkaController> logger)
    {
        _logger = logger;
    }

    [HttpPost]
    public async Task<ActionResult> CreateQuarantine(CreateQuarantineCommand request, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(request, cancellationToken);
        return Ok(result);
    }

    [HttpGet("active")]
    public async Task<ActionResult> GetActiveQuarantines([FromQuery] GetActiveQuarantinesQuery request, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(request, cancellationToken);
        return Ok(result);
    }

    [HttpPut]
    public async Task<ActionResult> Put(UpdateQuarantineCommand command, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(command, cancellationToken);
        return Ok(result);
    }
}
