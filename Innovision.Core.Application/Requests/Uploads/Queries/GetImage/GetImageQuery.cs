using Innovision.Core.Application.Common;
using MediatR;

namespace Innovision.Upload.Application.Requests.Uploads.Queries;

public record GetImageQuery(string UniqueFileName) : IRequest<ApiResponse<string>>;
