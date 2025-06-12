using Innovision.Core.Application.Common;
using MediatR;

namespace Innovision.Core.Application.Requests.Uploads.Commands;

public record UploadStringImageCommand(string Base64Image) : IRequest<ApiResponse<string>>;
