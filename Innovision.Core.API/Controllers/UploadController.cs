using Innovision.Core.API.Controllers;
using Innovision.Core.Application.Requests.Uploads.Commands;
using HappyPlay.Upload.Application.Requests.Uploads.Commands;
using HappyPlay.Upload.Application.Requests.Uploads.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HappyPlay.Upload.API.Controllers
{
    public class UploadController : ApiBaseController
    {
        private readonly ILogger<UploadController> _logger;

        public UploadController(ILogger<UploadController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// 
        /// Upload image
        /// 
        /// </summary>
        /// <param name="file"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> UploadImage([FromForm] IFormFile file, CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(new UploadImageCommand(file), cancellationToken);
            return Ok(result);
        }

        /// <summary>
        /// 
        /// Upload base64 image
        /// 
        /// </summary>
        /// <param name="command"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost("base64image")]
        [AllowAnonymous]
        public async Task<IActionResult> UploadBase64Image(UploadStringImageCommand command, CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(command, cancellationToken);
            return Ok(result);
        }

        /// <summary>
        /// 
        /// Get image
        /// 
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet("{fileName}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetImage(string fileName, CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(new GetImageQuery(fileName), cancellationToken);
            return Ok(result);
        }
        
    }
}
