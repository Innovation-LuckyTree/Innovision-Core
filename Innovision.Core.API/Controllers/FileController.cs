// // PLEASE NOTE: This is intended for apk downloading from SFTP server. This is not used in the production
// using System.Net;
// using Innovision.Core.API.Controllers;
// using Innovision.Core.Application.Common.Interfaces;
// using Microsoft.AspNetCore.Authorization;
// using Microsoft.AspNetCore.Mvc;
// using Renci.SshNet;

// namespace HappyPlay.Upload.API.Controllers;

// public class FileController(ILogger<UploadController> logger, ISftpConfig sftpConfig) : ApiBaseController
// {
//     private readonly ILogger<UploadController> _logger = logger;
//     private readonly ISftpConfig _sftpConfig = sftpConfig;

//     /// <summary>
//     /// use only in aws UAT
//     /// </summary>
//     /// <param name="fileName"></param>
//     /// <param name="cancellationToken"></param>
//     /// <returns></returns>
//     [HttpGet("apk/{fileName}")]
//     [AllowAnonymous]
//     public async Task<ActionResult> StreamFile(string fileName, CancellationToken cancellationToken)
//     {
//         try
//         {
//             // Open SFTP connection
//             using (var sftpClient = new SftpClient("dev-uat.sftp.esat-apps.com", _sftpConfig.Username, _sftpConfig.Password))
//             {
//                 sftpClient.Connect();

//                 if (!sftpClient.IsConnected)
//                 {
//                     return StatusCode((int)HttpStatusCode.InternalServerError, "Unable to connect to SFTP server.");
//                 }

//                 // Get file size
//                 var fileSize = sftpClient.GetAttributes($"/img-prof/apk/{fileName}").Size;

//                 // Stream the file directly to the response
//                 var outputStream = Response.Body;

//                 using (var remoteFileStream = sftpClient.OpenRead($"/img-prof/apk/{fileName}"))
//                 {
//                     Response.ContentType = "application/octet-stream";
//                     Response.Headers.Add("Content-Disposition", $"attachment; filename={fileName}");
//                     Response.Headers.Add("Content-Length", fileSize.ToString());

//                     // Buffer for streaming chunks
//                     var buffer = new byte[81920]; // 80 KB buffer
//                     int bytesRead;

//                     while ((bytesRead = remoteFileStream.Read(buffer, 0, buffer.Length)) > 0)
//                     {
//                         await outputStream.WriteAsync(buffer, 0, bytesRead, cancellationToken);
//                         await outputStream.FlushAsync(cancellationToken); // Flush to send data to the client
//                     }
//                 }

//                 sftpClient.Disconnect();
//             }

//             return new EmptyResult(); // No additional content is needed
//         }
//         catch (Exception ex)
//         {
//             return StatusCode((int)HttpStatusCode.InternalServerError, $"Error streaming file: {ex.Message}");
//         }
//     }
// }
