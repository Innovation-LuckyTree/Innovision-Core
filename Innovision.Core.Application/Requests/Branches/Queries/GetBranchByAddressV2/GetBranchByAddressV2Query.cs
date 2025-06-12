using System.ComponentModel.Design;
using Innovision.Core.Application.Common;
using MediatR;

namespace Innovision.Core.Application.Requests.Branches.Queries.GetBranchByAddress
{
    public class GetBranchByAddressQueryV2 : IRequest<ApiResponse<List<BranchInfoDto>>>
    {
        public string Region { get; set; }
        public string Province { get; set; }
    }
}
