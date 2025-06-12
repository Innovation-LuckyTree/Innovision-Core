using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Innovision.Core.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class ApiBaseController : ControllerBase
{
    private IMediator _mediator;
    
    protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();
}
