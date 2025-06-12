using Innovision.Core.Application.Requests.PsgLocations.Queries.GetBarangayByCityCode;
using Innovision.Core.Application.Requests.PsgLocations.Queries.GetBarangayByMunicipalityCode;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Innovision.Core.API.Controllers
{
    public class PsgcController : ApiBaseController
    {
        private readonly ILogger<PsgcController> _logger;

        public PsgcController(ILogger<PsgcController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// 
        /// Get barangay by citycode
        /// 
        /// </summary>
        /// <param name="cityCode"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet("cities/{cityCode}/barangays")]
        public async Task<ActionResult> GetBarangayByCityCode(string cityCode, CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(new GetBarangayByCityCodeQuery(cityCode), cancellationToken);
            return Ok(result);
        }

        /// <summary>
        /// 
        /// Get barangay by municipality
        /// 
        /// </summary>
        /// <param name="municipalityCode"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet("municipalities/{municipalityCode}/barangays")]
        public async Task<ActionResult> GetBarangayByMunicipalityCode(string municipalityCode, CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(new GetBarangayByMunicipalityCodeQuery(municipalityCode), cancellationToken);
            return Ok(result);
        }
    }
}
