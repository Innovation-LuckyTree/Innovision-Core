using Innovision.Core.Application.Common;
using Innovision.Core.Application.Common.Models;
using MediatR;

namespace Innovision.Core.Application.Requests.Branches.Commands.CreateBranch;

public class CreateBranchCommand : IRequest<ApiResponse<bool>>
{
    public Guid CompanyId { get; set; }
    public string BranchName { get; set; }
    public string MobileNumber { get; set; }
    public string Region { get; set; }
    public string Province { get; set; }
    public string Municipality { get; set; }
    public string Barangay { get; set; }
    public string StreetOrPurok { get; set; }
    public AddressCodes? AddressCode { get; set; }
}
