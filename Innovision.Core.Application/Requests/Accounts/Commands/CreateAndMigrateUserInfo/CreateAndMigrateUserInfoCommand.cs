using MediatR;

namespace Innovision.Core.Application.Requests.Accounts.Commands.CreateAndMigrateUserInfo;

public class CreateAndMigrateUserInfoCommand : IRequest<Unit>
{
    public string ReferralCode { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string MobileNumber { get; set; }
}
