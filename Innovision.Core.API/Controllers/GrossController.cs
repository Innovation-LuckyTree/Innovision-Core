using Innovision.Core.Application.Requests.Orders.Queries.GetOrderItemDetail;
using Microsoft.AspNetCore.Mvc;

namespace Innovision.Core.API.Controllers;

public class GrossController : ApiBaseController
{
    [HttpGet]
    public async Task<ActionResult> GetCompanyGross([FromQuery]GetOrderGrossQuery request, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(request, cancellationToken);
        return Ok(result);
    }
}
