using Innovision.Core.Application.Requests.Branches.Commands.CreateBranch;
using Innovision.Core.Application.Requests.Branches.Commands.UpdateBranch;
using Innovision.Core.Application.Requests.Branches.Commands.UpdateBranchDefaultAgents;
using Innovision.Core.Application.Requests.Branches.Queries.GetAccountsByBranchId;
using Innovision.Core.Application.Requests.Branches.Queries.GetBranchByAddress;
using Innovision.Core.Application.Requests.Branches.Queries.GetBranchById;
using Innovision.Core.Application.Requests.Branches.Queries.GetBranchByReferralCode;
using Innovision.Core.Application.Requests.Branches.Queries.GetBranches;
using Innovision.Core.Application.Requests.Branches.Queries.GetMainBranch;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Innovision.Core.API.Controllers;

public class BranchController : ApiBaseController
{

    private readonly ILogger<BranchController> _logger;

    public BranchController(ILogger<BranchController> logger)
    {
        _logger = logger;
    }

    /// <summary>
    /// Get all branches
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet]
    public async Task<ActionResult> Get(CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(new GetBranchesQuery(), cancellationToken);
        return (result.Success) ? Ok(result) : BadRequest(result);
    }

    /// <summary>
    /// Get list of all branches by page
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    [HttpPost("search")]
    public async Task<IActionResult> Post([FromBody] GetBranchesQuery command, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(command, cancellationToken);
        return (result.Success) ? Ok(result) : BadRequest(result);
    }

    /// <summary>
    /// Get main office by company object id
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    [HttpGet("main")]
    public async Task<IActionResult> GetMainBranch([FromQuery] GetMainBranchQuery request, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(request, cancellationToken);
        return (result.Success) ? Ok(result) : BadRequest(result);
    }

    /// <summary>
    /// Get Branch by ID
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet("{branchId}")]
    [AllowAnonymous]
    public async Task<ActionResult> GetByBranchId(int branchId, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(new GetBranchByIdQuery(branchId), cancellationToken);
        return (result.Success) ? Ok(result) : BadRequest(result);
    }

    /// <summary>
    /// 
    /// Get branch by user referral code
    /// 
    /// </summary>
    /// <param name="referral"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet("referral/{referralcode}")]
    [AllowAnonymous]
    public async Task<ActionResult> GetBranchByReferral(string referralcode, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(new GetBranchByReferralCodeQuery(referralcode), cancellationToken);
        return (result.Success) ? Ok(result) : BadRequest(result);
    }

    /// <summary>
    /// 
    /// Get branch list by address
    /// 
    /// </summary>
    /// <param name="command"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost("address")]
    [AllowAnonymous]
    public async Task<ActionResult> GetBranchByAdddress(GetBranchByAddressQuery command, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(command, cancellationToken);
        return (result.Success) ? Ok(result) : BadRequest(result);
    }
    [HttpPost("address/v2")]
    [AllowAnonymous]
    public async Task<ActionResult> GetBranchByAdddressV2(GetBranchByAddressQueryV2 command, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(command, cancellationToken);
        return (result.Success) ? Ok(result) : BadRequest(result);
    }

    /// <summary>
    /// Create new branch
    /// </summary>
    /// <param name="createBranchCommand"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// 
    [HttpPost]
    public async Task<ActionResult> Post(CreateBranchCommand createBranchCommand, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(createBranchCommand, cancellationToken);
        return (result.Success) ? Ok(result) : BadRequest(result);
    }

    /// <summary>
    /// Update branch endpoint
    /// </summary>
    /// <param name="command"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPut]
    public async Task<ActionResult> Put(UpdateBranchCommand command, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(command, cancellationToken);
        return (result.Success) ? Ok(result) : BadRequest(result);
    }

    /// <summary>
    /// Update branch Default Agent
    /// </summary>
    /// <param name="command"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPatch("agent")]
    public async Task<ActionResult> UpdateBranchAgent(UpdateBranchDefaultAgentsCommand command, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(command, cancellationToken);
        return (result.Success) ? Ok(result) : BadRequest(result);
    }

    [HttpPost("accounts")]
    public async Task<IActionResult> GetAccountsUnderBranch([FromBody] GetAccountsByBranchIdQuery query, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(query, cancellationToken);
        return Ok(result);
    }
}
