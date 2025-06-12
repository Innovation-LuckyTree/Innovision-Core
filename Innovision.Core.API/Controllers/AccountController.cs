using Innovision.Core.Application.Requests.Accounts.Commands.AddAccountToUserIdentity;
using Innovision.Core.Application.Requests.Accounts.Commands.CreateAndMigrateUserInfo;
using Innovision.Core.Application.Requests.Accounts.Commands.ResetUserPassword;
using Innovision.Core.Application.Requests.Accounts.Commands.SetPaymentProvider;
using Innovision.Core.Application.Requests.Accounts.Commands.UpdateUserPassword;
using Innovision.Core.Application.Requests.Accounts.Queries.GetAccountByMobileNumber;
using Innovision.Core.Application.Requests.Accounts.Queries.GetAccountByPaymentAccount;
using Innovision.Core.Application.Requests.Accounts.Queries.GetAccountByUsername;
using Innovision.Core.Application.Requests.Accounts.Queries.GetAccountInfoByUserId;
using Innovision.Core.Application.Requests.Accounts.Queries.GetAccountList;
using Innovision.Core.Application.Requests.Accounts.Queries.GetApprovedAccounts;
using Innovision.Core.Application.Requests.Accounts.Queries.GetCurrentAccount;
using Innovision.Core.Application.Requests.Accounts.Queries.GetCurrentAccountInfo;
using Innovision.Core.Application.Requests.Accounts.Queries.GetCurrentSystemUser;
using Innovision.Core.Application.Requests.Accounts.Queries.GetDownlineAccountIds;
using Innovision.Core.Application.Requests.Accounts.Queries.GetUnverifiedUsersFor7Days;
using Innovision.Core.Application.Requests.Accounts.Queries.GetUserToken;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Innovision.Core.API.Controllers;

public class AccountController : ApiBaseController
{
    private readonly ILogger<AccountController> _logger;

    public AccountController(ILogger<AccountController> logger)
    {
        _logger = logger;
    }

    /// <summary>
    /// Get account by username 
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet("profile")]
    public async Task<ActionResult> Get(string username, bool isPlayer, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(new GetAccountByUsernameQuery(username) { IsPlayer = isPlayer }, cancellationToken);
        return Ok(result);
    }

    /// <summary>
    /// Get account info by token
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet("current")]
    public async Task<ActionResult> GetCurrentAccount(CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(new GetCurrentAccountQuery(), cancellationToken);
        return Ok(result);
    }

    /// <summary>
    /// Get account info by token
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet("system/current")]
    public async Task<ActionResult> GetCurrentSystemUserQuery(CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(new GetCurrentSystemUserQuery(), cancellationToken);
        return Ok(result);
    }

    /// <summary>
    /// Get account info by token
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet("current/info")]
    public async Task<ActionResult> GetCurrentAccountInfo(CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(new GetCurrentAccountInfoQuery(), cancellationToken);
        return Ok(result);
    }

    /// <summary>
    /// Get Account information list
    /// </summary>
    /// <param name="query"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost("info/list")]
    public async Task<ActionResult> GetAccountInformationList(GetAccountListQuery query, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(query, cancellationToken);
        return Ok(result);
    }

    /// <summary>
    /// Get account info by Account Id
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet("info/{accountObjectId}")]
    public async Task<ActionResult> GetCurrentAccountInfoById(Guid accountObjectId, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(new GetCurrentAccountInfoByAccountIdQuery(accountObjectId), cancellationToken);
        return Ok(result);
    }

    /// <summary>
    /// Get account info by Account Id
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet("{accountId}/info")]
    public async Task<ActionResult> GetAccountInfoByLongId(Guid accountId, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(new GetCurrentAccountInfoByAccountIdQuery(accountId), cancellationToken);
        return Ok(result);
    }

    /// <summary>
    /// Get account info by payment Account
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet("payment-account/{paymentAccountId}")]
    public async Task<ActionResult> GetCurrentAccount(string paymentAccountId, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(new GetAccountByPaymentAccountQuery(paymentAccountId), cancellationToken);
        return Ok(result);
    }

    /// <summary>
    /// Get account info by payment Account
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet("user-info/{userId}")]
    public async Task<ActionResult> GetAccountInfoByUserId(Guid userId, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(new GetAccountInfoByUserIdQuery(userId), cancellationToken);
        return Ok(result);
    }

    /// <summary>
    /// Get account by username 
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet("details/{username}/{isPlayer}")]
    public async Task<ActionResult> GetUserDetails(string username, bool isPlayer, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(new GetAccountByUsernameQuery(username) { IsPlayer = isPlayer }, cancellationToken);
        return Ok(result);
    }

    /// <summary>
    /// Get account by username 
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet("details/{mobileNumber}")]
    public async Task<ActionResult> GetUserDetailsByMobileNumebr(string mobileNumber, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(new GetAccountByMobileNumberQuery(mobileNumber), cancellationToken);
        return Ok(result);
    }

    [HttpPost("details/list")]
    public async Task<ActionResult> GetUserInformationList(string mobileNumber, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(new GetAccountByMobileNumberQuery(mobileNumber), cancellationToken);
        return Ok(result);
    }

    [HttpPost("user")]
    public async Task<IActionResult> CreateUserIdentity(AddAccountToUserIdentityCommand request, CancellationToken cancellationToken)
    {
        var response = await Mediator.Send(request, cancellationToken);

        return Ok(response);
    }

    [HttpPost("fullcreation")]
    public async Task<IActionResult> CreateAndMigrateUserInfo(CreateAndMigrateUserInfoCommand request, CancellationToken cancellationToken)
    {
        var response = await Mediator.Send(request, cancellationToken);

        return Ok(response);
    }

    [HttpGet("approved")]
    public async Task<IActionResult> GetApprovedAccounts(CancellationToken cancellationToken)
    {
        var response = await Mediator.Send(new GetApprovedAccountsQuery(), cancellationToken);

        return Ok(response);
    }


    [HttpPost("auth")]
    [AllowAnonymous]
    public async Task<IActionResult> GetToken(GetUserTokenCommand request, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(request, cancellationToken);
        return (result.Success) ? Ok(result) : BadRequest(result);
    }

    [HttpPost("new/password")]
    [AllowAnonymous]
    public async Task<IActionResult> UpdateUserPassword(UpdateUserPasswordCommand request, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(request, cancellationToken);
        return Ok(result);
    }

    [HttpPost("password/reset")]
    [AllowAnonymous]
    public async Task<IActionResult> ResetUserPassword(ResetUserPasswordCommand request, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(request, cancellationToken);
        return Ok(result);
    }

    [HttpPost("payment-provider")]
    public async Task<IActionResult> SetPaymentProvider(CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(new SetPaymentProviderCommand(), cancellationToken);
        return Ok(result);
    }


    [HttpGet("unverified/players/sevendays")]
    public async Task<ActionResult> GetUnverifiedPlayersForSevenDays(CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(new GetUnverifiedUsersFor7DaysQuery(), cancellationToken);
        return Ok(result);
    }

    [HttpGet("downlines/{accountId}")]
    public async Task<ActionResult> GetAccountDownlines(long accountId, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(new GetDownlineAccountIdsQuery(accountId), cancellationToken);
        return Ok(result);
    }
}