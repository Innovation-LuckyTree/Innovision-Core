using Innovision.Core.Application.Requests.Roles.Commands;
using Innovision.Core.Application.Requests.Roles.Queries.GetRoles;
using Innovision.Core.Application.Requests.Roles.Queries.GetRolesByGroupType;
using Microsoft.AspNetCore.Mvc;

namespace Innovision.Core.API.Controllers
{
    public class RolesController : ApiBaseController
    {
        private readonly ILogger<RolesController> _logger;

        public RolesController(ILogger<RolesController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// 
        /// Get roles 
        /// 
        /// </summary>
        /// <param name="CompanyId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet("{CompanyId?}")]
        public async Task<IActionResult> GetRolesQuery(int? CompanyId, CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(new GetRolesQuery(CompanyId), cancellationToken);
            return (result.Success) ? Ok(result) : BadRequest(result);
        }

        /// <summary>
        /// 
        /// Create security group menu by company and usertype
        /// 
        /// </summary>
        /// <param name="command"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost("grouptype")]
        public async Task<IActionResult> GetRolesByGroupType(GetRolesByGroupTypeQuery command, CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(command, cancellationToken);
            return (result.Success) ? Ok(result) : BadRequest(result);
        }

        /// <summary>
        /// 
        /// Remove roles
        /// 
        /// </summary>
        /// <param name="command"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteRole([FromQuery]DeleteRoleCommand command, CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(command, cancellationToken);
            return (result.Success) ? Ok(result) : BadRequest(result);
        }
    }
}
