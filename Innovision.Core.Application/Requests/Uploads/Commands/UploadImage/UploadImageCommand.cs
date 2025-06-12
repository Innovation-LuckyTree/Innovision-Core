using Innovision.Core.Application.Common;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace HappyPlay.Upload.Application.Requests.Uploads.Commands;

public record UploadImageCommand(IFormFile FileRequest) : IRequest<ApiResponse<string>>;
