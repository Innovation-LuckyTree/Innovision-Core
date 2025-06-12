using Innovision.Core.Application.Requests.Withdrawals.Commands.AddWithdrawalByAccounting;
using Innovision.Core.Application.Requests.Withdrawals.Commands.AddWithdrawalByUsers;
using Innovision.Core.Application.Requests.Withdrawals.Commands.ProcessWithdrawal;
using Innovision.Core.Application.Requests.Withdrawals.Queries.GetCurrentAccountWithdrawal;
using Innovision.Core.Application.Requests.Withdrawals.Queries.GetWithdrawal;
using Innovision.Core.Application.Requests.Withdrawals.Queries.GetWithdrawalByAccountInfoId;
using Innovision.Core.Application.Requests.Withdrawals.Queries.GetWithdrawalByTransactionNo;
using Innovision.Core.Application.Requests.Withdrawals.Queries.GetWithdrawalExport;
using Innovision.Core.Application.Requests.Withdrawals.Queries.GetWithrawalDetailById;
using Microsoft.AspNetCore.Mvc;

namespace Innovision.Core.API.Controllers;

public class WithdrawalController : ApiBaseController
{
    private readonly ILogger<WithdrawalController> _logger;

    public WithdrawalController(ILogger<WithdrawalController> logger)
    {
        _logger = logger;
    }

    [HttpGet("details/{transactionNo}")]
    public async Task<IActionResult> GetByTransactionNo(long transactionNo, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(new GetWithrawalDetailByIdQuery(transactionNo), cancellationToken);

        return Ok(result);
    }

    [HttpPost("account/current")]
    public async Task<ActionResult> GetCurrentAccountWithdrawals(GetCurrentAccountWithdrawalQuery request, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(request, cancellationToken);
        return Ok(result);
    }

    /// <summary>
    /// Get list of all withdrawal by the current account
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    [HttpGet("current")]
    public async Task<IActionResult> GetCurrentAccount([FromBody] GetWithdrawalQuery command, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(command, cancellationToken);
        return (result.Success) ? Ok(result) : BadRequest(result);
    }

    /// <summary>
    /// Get list of all withdrawal by page
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    [HttpPost("search")]
    public async Task<IActionResult> Post([FromBody] GetWithdrawalQuery command, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(command, cancellationToken);
        return (result.Success) ? Ok(result) : BadRequest(result);
    }
    [HttpPost("list/export")]
    public async Task<IActionResult> GetWithdrawalListExport(GetWithdrawalExportQuery request, CancellationToken cancellationToken){
        var response = await Mediator.Send(request,cancellationToken);
        return Ok(response);
    }

    /// <summary>
    /// Get Withdrawal by AccountObjectId
    /// </summary>
    /// <param name="command"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost("searchByAccountId")]
    public async Task<IActionResult> GetWithdrawalByID(GetWithdrawalByAccountInfoIdQuery command, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(command, cancellationToken);
        return (result.Success) ? Ok(result) : BadRequest(result);
    }

    /// <summary>
    /// Get company profile including list of branches
    /// </summary>
    /// <param name="transactionNo"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet("{transactionNo}")]
    public async Task<IActionResult> GetByTransactionNo(string transactionNo, CancellationToken cancellationToken)
    {
        var query = new GetWithdrawalByTransactionNoQuery(transactionNo) { };
        var result = await Mediator.Send(query, cancellationToken);

        return (result.Success) ? Ok(result) : BadRequest(result);
    }

    /// <summary>
    /// Create Withdrawal by Accounting
    /// </summary>
    /// <param name="addWithdrawalByAccountingCommand"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost("accounting")]
    public async Task<ActionResult> Post(AddWithdrawalByAccountingCommand addWithdrawalByAccountingCommand, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(addWithdrawalByAccountingCommand, cancellationToken);
        return (result.Success) ? Ok(result) : BadRequest(result);
    }

    /// <summary>
    /// Create Withdrawal
    /// </summary>
    /// <param name="addWithdrawalByUsersCommand"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<ActionResult> Post(AddWithdrawalByUsersCommand addWithdrawalByUsersCommand, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(addWithdrawalByUsersCommand, cancellationToken);
        return Ok(result);
    }

    /// <summary>
    /// Create Withdrawal
    /// </summary>
/// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPut("status")]
    public async Task<ActionResult> UpdateStatus(UpdateWithdrawalStatusCommand request, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(request, cancellationToken);
        return Ok(result);
    }

    /// <summary>
    /// Process existing Withdrawal
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPut("process")]
    public async Task<ActionResult> ProcessWithdrawal(ProcessWithdrawalCommand request, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(request, cancellationToken);
        return Ok(result);
    }
}
