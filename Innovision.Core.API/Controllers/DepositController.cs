using Innovision.Core.Application.Requests.Deposits.Commands.AddUserDepositRequest;
using Innovision.Core.Application.Requests.Deposits.Queries.GetDepositById;
using Innovision.Core.Application.Requests.Deposits.Queries.GetDepositStatus;
using Innovision.Core.Application.Requests.Deposits.Queries.LookupReference;
using Innovision.Core.Application.Requests.Deposits.Queries.SearchDepositList;
using Innovision.Core.Application.Requests.Deposits.Queries.SearchDepositListExport;
using Microsoft.AspNetCore.Mvc;

namespace Innovision.Core.API.Controllers;

public class DepositController : ApiBaseController
{
    private readonly ILogger<DepositController> _logger;

    public DepositController(ILogger<DepositController> logger)
    {
        _logger = logger;
    }

    [HttpGet("status")]
    public async Task<ActionResult> GetDepositStatus(CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(new GetDepositStatusQuery(), cancellationToken);
        return Ok(result);
    }


    [HttpGet("{depositId}")]
    public async Task<ActionResult> GetDepositById(long depositId, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(new GetDepositByIdQuery(depositId), cancellationToken);
        return Ok(result);
    }

    [HttpGet("reference/search/{transactionNo}")]
    public async Task<ActionResult> SearchTransctionNo(string transactionNo, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(new LookupReferenceQuery(transactionNo), cancellationToken);
        return Ok(result);
    }

    [HttpPost("search")]
    public async Task<ActionResult> Search([FromBody] SearchDepositListQuery query, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(query, cancellationToken);
        return Ok(result);
    }

    [HttpPost("search/export")]
    public async Task<ActionResult> SearchExport([FromBody] SearchDepositListExportQuery query, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(query, cancellationToken);
        return Ok(result);
    }

    [HttpPost("request")]
    public async Task<ActionResult> AddUserDepositRequest([FromBody] AddUserDepositRequestCommand query, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(query, cancellationToken);
        return Ok(result);
    }

    /// <summary>
    /// Save User deposit from payment provider callback
    /// </summary>
    /// <param name="command"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost("user/transaction")]
    public async Task<ActionResult> SaveUserDepositTransaction([FromBody] SaveUserDepositTransactionCommand command, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(command, cancellationToken);
        return Ok(result);
    }

    [HttpPut("update/status")]
    public async Task<ActionResult> UpdateDepositStatus([FromBody] UpdateDepositStatusCommand command, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(command, cancellationToken);
        return Ok(result);
    }
}
