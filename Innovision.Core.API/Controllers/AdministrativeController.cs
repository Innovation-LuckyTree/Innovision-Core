using Innovision.Core.Application.Requests.Administratives.Queries.GetAdminExclusionsExport;
using Innovision.Core.Application.Requests.Administratives.Queries.GetSelfLimitsExport;
using Innovision.Core.Application.Requests.ApplicationVersions.Commands.CreateAdministrativeExclusion;
using Innovision.Core.Application.Requests.ApplicationVersions.Commands.CreateSelfLimit;
using Innovision.Core.Application.Requests.ApplicationVersions.Commands.UpdateAdministrativeExclusion;
using Innovision.Core.Application.Requests.ApplicationVersions.Commands.UpdateSelfLimit;
using Innovision.Core.Application.Requests.ApplicationVersions.Queries.GetAccountAdminExclusion;
using Innovision.Core.Application.Requests.ApplicationVersions.Queries.GetAccountSelfLimit;
using Innovision.Core.Application.Requests.ApplicationVersions.Queries.GetAdminExclusionById;
using Innovision.Core.Application.Requests.ApplicationVersions.Queries.GetAdminExclusions;
using Innovision.Core.Application.Requests.ApplicationVersions.Queries.GetSelfLimitById;
using Innovision.Core.Application.Requests.ApplicationVersions.Queries.GetSelfLimits;
using Microsoft.AspNetCore.Mvc;

namespace Innovision.Core.API.Controllers;

public class AdministrativeController(ILogger<AdministrativeController> logger) : ApiBaseController
{
    private readonly ILogger<AdministrativeController> _logger = logger;


    [HttpGet("account/limit/{accountId}")]
    public async Task<IActionResult> GetAccountLimit(long accountId, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(new GetAccountLimitQuery(accountId), cancellationToken);

        return Ok(result);
    }

    [HttpGet("exclusion/account/{accountId}")]
    public async Task<IActionResult> GeAccountAdminExclusion(long accountId, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(new GetAccountAdminExclusionQuery(accountId), cancellationToken);

        return Ok(result);
    }

    [HttpPost("exclusion/list")]
    public async Task<IActionResult> GetExclusionList(GetAdminExclusionsQuery request, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(request, cancellationToken);

        return Ok(result);
    }

    [HttpPost("exclusion/list/export")]
    public async Task<IActionResult> GetExclusionListExport(GetAdminExclusionsExportQuery request, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(request, cancellationToken);
        return Ok(result);
    }

    [HttpGet("exclusion/{id}")]
    public async Task<IActionResult> GetExclusionById(int id, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(new GetAdminExclusionByIdQuery(id), cancellationToken);

        return Ok(result);
    }

    [HttpPost("exclusion")]
    public async Task<IActionResult> CreateExclusion(CreateAdministrativeExclusionCommand request, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(request, cancellationToken);

        return Ok(result);
    }

    [HttpPatch("exclusion")]
    public async Task<IActionResult> UpdateExclusion(UpdateAdministrativeExclusionCommand request, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(request, cancellationToken);

        return Ok(result);
    }

    [HttpGet("self-limit/account/{accountId}")]
    public async Task<IActionResult> GetAccountSelfLimit(long accountId, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(new GetAccountSelfLimitQuery(accountId), cancellationToken);

        return Ok(result);
    }

    [HttpPost("self-limit/list")]
    public async Task<IActionResult> GetSelfLimitList(GetSelfLimitsQuery request, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(request, cancellationToken);

        return Ok(result);
    }

    [HttpPost("self-limit/list/export")]
    public async Task<IActionResult> GetSelfLimitListExport(GetSelfLimitsExportQuery request, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(request, cancellationToken);
        return Ok(result);
    }

    [HttpGet("self-limit/{id}")]
    public async Task<IActionResult> GetSelfLimitById(int id, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(new GetSelfLimitByIdQuery(id), cancellationToken);

        return Ok(result);
    }

    [HttpPost("self-limit")]
    public async Task<IActionResult> CreateSelfLimit(CreateSelfLimitCommand request, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(request, cancellationToken);

        return Ok(result);
    }

    [HttpPatch("self-limit")]
    public async Task<IActionResult> UpdateSelfLimit(UpdateSelfLimitCommand request, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(request, cancellationToken);

        return Ok(result);
    }
}
