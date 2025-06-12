using Innovision.Core.Application.Exceptions;
using Microsoft.AspNetCore.Http;

namespace Innovision.Core.Application.Common.Extensions;

public static class FileUploadExtensions
{
    public static string ToBase64String(this IFormFile formFile)
    {
        string base64Image = string.Empty;

        var content = formFile.ContentType;

        using MemoryStream memoryStream = new();

        try
        {
            formFile.CopyTo(memoryStream);
            var fileBytes = memoryStream.ToArray();
            base64Image = $"data:{content};base64, {Convert.ToBase64String(fileBytes)}";
        }
        catch (Exception ex)
        {
            throw new FileServiceException("Base 64 Conversion", ex.Message);
        }
        finally
        {
            memoryStream.Dispose();
        }

        return base64Image;
    }
}