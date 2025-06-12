using Innovision.Core.Application.Common;
using MediatR;

namespace Innovision.Core.Application.Requests.Users.Commands.BulkCreateUser
{
    public class BulkCreateUserCommand : IRequest<ApiResponse<List<BulkCreateDto>>>
    {
        public int BranchId { get; set; }
        public string ReferralCode { get; set; }
        public int UserTypeId { get; set; }
        public List<BulkUser> Users { get; set; }
    }

    public class BulkUser
    {
        public string MobileNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public decimal Commission { get; set; }
        public int? Position { get; set; }
    }
}
