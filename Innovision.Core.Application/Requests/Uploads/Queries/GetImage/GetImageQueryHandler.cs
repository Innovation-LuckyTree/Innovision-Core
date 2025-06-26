using Innovision.Core.Application.Common;
using Innovision.Core.Application.Common.Interfaces;
using MediatR;

namespace Innovision.Upload.Application.Requests.Uploads.Queries;

public class GetImageQueryHandler(IFileService fileService) : IRequestHandler<GetImageQuery, ApiResponse<string>>
{
    private readonly IFileService _fileService = fileService;

    public async Task<ApiResponse<string>> Handle(GetImageQuery request, CancellationToken cancellationToken)
    {
        var result = await _fileService.GetBase64Image(request.UniqueFileName);
        return new ApiResponse<string>() { Data = result.FileContent };
    }
}
