using Innovision.Core.Application.Requests.PaymentMethods.Commands.CreatePaymentMethod;
using Innovision.Core.Application.Requests.PaymentMethods.Commands.UpdatePaymentMethod;
using Innovision.Core.Application.Requests.PaymentMethods.Queries.GetPaymentMethods;
using Microsoft.AspNetCore.Mvc;

namespace Innovision.Core.API.Controllers;

public class PaymentMethodController : ApiBaseController
{
    private readonly ILogger<PaymentMethodController> _logger;

    public PaymentMethodController(ILogger<PaymentMethodController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> GetPaymentMethods(CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(new GetPaymentMethodsQuery(), cancellationToken);
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> CreatePaymentMethod(CreatePaymentMethodCommand request, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(request, cancellationToken);
        return Ok(result);
    }

    [HttpPatch]
    public async Task<IActionResult> UpdatePaymentMethod(UpdatePaymentMethodCommand request, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(request, cancellationToken);
        return Ok(result);
    }
}
