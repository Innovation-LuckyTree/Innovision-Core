using Innovision.Core.Application.Common;
using MediatR;

namespace Innovision.Core.Application.Requests.Accounts.Users.AgentAndPlayer.Commands.UpdateUser;

public class UpdateUserCommand : IRequest<ApiResponse<Unit>>
{
    public Guid AccountObjectId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string MiddleName { get; set; }
    public string Email { get; set; }
    public string Gender { get; set; }
    public string MartialStatus { get; set; }
    public string BloodType { get; set; }
    public string Nationality { get; set; }
    public string NatureOfWork { get; set; }
    public string SourceOfIncome { get; set; }
    public int AccountStatusId { get; set; }

    public string ValidId { get; set; }
    public string FrontIdPath { get; set; }
    public string BackIdPath { get; set; }
    public string SignaturePath { get; set; }
    public string ProfilePath { get; set; }
    public string SelfiePath { get; set; }

    public string Region { get; set; }
    public string Province { get; set; }
    public string Municipality { get; set; }
    public string Barangay { get; set; }
    public string StreetOrPurok { get; set; }

    public string PresentRegion { get; set; }
    public string PresentProvince { get; set; }
    public string PresentMunicipality { get; set; }
    public string PresentBarangay { get; set; }
    public string PresentStreetOrPurok { get; set; }

    public string PermanentRegion { get; set; }
    public string PermanentProvince { get; set; }
    public string PermanentMunicipality { get; set; }
    public string PermanentBarangay { get; set; }
    public string PermanentStreetOrPurok { get; set; }
}
