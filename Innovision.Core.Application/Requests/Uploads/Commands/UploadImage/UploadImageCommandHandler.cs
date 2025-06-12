using Innovision.Core.Application.Common;
using Innovision.Core.Application.Common.Extensions;
using Innovision.Core.Application.Common.Interfaces;
using MediatR;

namespace HappyPlay.Upload.Application.Requests.Uploads.Commands;

public class UploadImageCommandHandler(IFileService fileService) : IRequestHandler<UploadImageCommand, ApiResponse<string>>
{
    private readonly IFileService _fileService = fileService;


    public async Task<ApiResponse<string>> Handle(UploadImageCommand request, CancellationToken cancellationToken)
    {
        if (request.FileRequest.Length == 0)
            return new ApiResponse<string>() { Success = false, ErrorMessage = "No image found." };

        var base64Image = request.FileRequest.ToBase64String();
        var result = await _fileService.UploadImage(base64Image);

        return new ApiResponse<string>() { Data = result };
    }
}
