using Innovision.Core.Application.Wallets.Commands.CreateWalletSettings;
using Innovision.Core.Application.Wallets.Commands.UpdateDepositSettings;
using Innovision.Core.Application.Wallets.Commands.UpdateWithdrawalSettings;
using Microsoft.AspNetCore.Mvc;

namespace Innovision.Core.API.Controllers;

public class WalletController : ApiBaseController
{
    private readonly ILogger<WalletController> _logger;

    public WalletController(ILogger<WalletController> logger)
    {
        _logger = logger;
    }

    [HttpPost("settings")]
    public async Task<ActionResult> CreateWalletSettings(CreateWalletSettingsCommand request, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(request, cancellationToken);
        return Ok(result);
    }

    [HttpPost("settings/deposit")]
    public async Task<ActionResult> UpdateDepositSettings(UpdateDepositSettingsCommand request, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(request, cancellationToken);
        return Ok(result);
    }

    [HttpPost("settings/withdraw")]
    public async Task<ActionResult> UpdateWithdrawSettings(UpdateWithdrawalSettingsCommand request, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(request, cancellationToken);
        return Ok(result);
    }
}
