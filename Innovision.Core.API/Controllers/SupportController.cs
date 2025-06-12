using Innovision.Core.Application.Requests.Support.Queries.ExportCaseItems;
using Innovision.Core.Application.Requests.Support.Queries.GetCaseItems;
using Microsoft.AspNetCore.Mvc;

namespace Innovision.Core.API.Controllers;

public class SupportController : ApiBaseController
{
    private readonly ILogger<SupportController> _logger;

    public SupportController(ILogger<SupportController> logger)
    {
        _logger = logger;
    }


    [HttpPost ("cases")]
    public async Task<ActionResult> GetCases([FromBody] GetCaseItemsQuery request, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(request, cancellationToken);
        return Ok(result);
    }

    [HttpPost ("cases/export")]
    public async Task<ActionResult> ExportCaseItems([FromBody] ExportCaseItemsQuery request, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(request, cancellationToken);
        return Ok(result);
    }


}