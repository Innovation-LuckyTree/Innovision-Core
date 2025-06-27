using Innovision.Core.Application.Requests.AccountSettings.Queries;
using Microsoft.AspNetCore.Mvc;

namespace Innovision.Core.API.Controllers;

public class AccountSettingController : ApiBaseController
{
    private readonly ILogger<AccountSettingController> _logger;

    public AccountSettingController(ILogger<AccountSettingController> logger)
    {
        _logger = logger;
    }

    /// <summary>
    /// Get account setting 
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet]
    public async Task<ActionResult> Get(CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(new GetUserAccountSettingQuery(), cancellationToken);
        return Ok(result);
    }
}
