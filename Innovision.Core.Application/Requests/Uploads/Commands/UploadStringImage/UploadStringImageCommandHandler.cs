using Innovision.Core.Application.Common;
using Innovision.Core.Application.Common.Interfaces;
using MediatR;

namespace Innovision.Core.Application.Requests.Uploads.Commands;

public class UploadStringImageCommandHandler(IFileService fileService) : IRequestHandler<UploadStringImageCommand, ApiResponse<string>>
{
    private readonly IFileService _fileService = fileService;

    public async Task<ApiResponse<string>> Handle(UploadStringImageCommand request, CancellationToken cancellationToken)
    {
        var result = await _fileService.UploadImage(request.Base64Image);
        
        return new ApiResponse<string>() { Data = result };
    }
}
