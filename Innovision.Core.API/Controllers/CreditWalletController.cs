using Innovision.Core.Application.Requests.CreditWallets.Queries.GetAccountTransactions;
using Innovision.Core.Infrastructure.PaymentServices.Models.Requests;
using Microsoft.AspNetCore.Mvc;

namespace Innovision.Core.API.Controllers;

public class CreditWalletController : ApiBaseController
{
    private readonly ILogger<CreditWalletController> _logger;

    public CreditWalletController(ILogger<CreditWalletController> logger)
    {
        _logger = logger;
    }

    [HttpPost("list")]
    public async Task<ActionResult> GetAccountTransactions([FromBody] GetAccountTransactionRequest request, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(new GetAccountTransactionsQuery(request), cancellationToken);
        return Ok(result);
    }
}
