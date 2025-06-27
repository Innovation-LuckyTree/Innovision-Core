using Innovision.Core.Application.Common;
using Innovision.Core.Application.Common.Models;
using MediatR;

namespace Innovision.Core.Application.Requests.Accounts.Users.AgentAndPlayer.Commands.UserRegistration;

public class UserRegistrationCommand : IRequest<ApiResponse<Guid>>
{
    public string? ReferralCode { get; set; }
    public string MobileNumber { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string MiddleName { get; set; }
    public string Nationality { get; set; }
    public string NatureOfWork { get; set; }
    public string SourceOfIncome { get; set; }
    public string BirthDate { get; set; }
    public string PlaceOfBirth { get; set; }
    public string? Sex { get; set; }
    public string? CivilStatus { get; set; }
    public int? BranchId { get; set; }
    public decimal? Commission { get; set; }
    public int? SalaryRange { get; set; }

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

    public AddressCodes? AddressCode { get; set; }

    public string ValidId { get; set; }
    public string FrontIdPath { get; set; }
    public string SelfiePath { get; set; }
    public string BackIdPath { get; set; }
    public string SignaturePath { get; set; }
}
