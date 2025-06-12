using Innovision.Core.Application.Common.Models;

namespace Innovision.Core.Application.Common.Interfaces;

public interface IFileService
{
    Task<string> UploadImage(string base64Image);
    Task<Base64FileResponse> GetBase64Image(string fileName);
}
