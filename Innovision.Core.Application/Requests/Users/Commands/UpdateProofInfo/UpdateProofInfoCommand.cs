using Innovision.Core.Application.Common;
using MediatR;

namespace Innovision.Core.Application.Requests.Users.Commands.UpdateProofInfo;

public class UpdateProofInfoCommand : IRequest<ApiResponse<bool>>
{
    public string ValidIdType { get; set; }
    public string FrontIdPath { get; set; }
    public string BackIdPath { get; set; }
    public string SelfiePath { get; set; }
    public string? SignaturePath { get; set; }
}
