using Innovision.Core.Application.Common;
using MediatR;

namespace Innovision.Core.Application.Requests.Users.Commands.BasicVerification
{
    public class BasicVerificationCommand : IRequest<ApiResponse<bool>>
    {
        public Guid AccountObjectId { get; set; }
        public string MobileNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string Suffix { get; set; }
        public string NatureOfWork { get; set; }
        public string SourceOfIncome { get; set; }
        public string BirthDate { get; set; }
        public int? SalaryRange { get; set; }

        public string FrontIdPath { get; set; }
        public string SelfiePath { get; set; }
        public string BackIdPath { get; set; }
    }
}
