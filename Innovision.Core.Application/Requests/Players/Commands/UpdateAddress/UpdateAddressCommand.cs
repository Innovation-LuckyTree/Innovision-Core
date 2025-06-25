using Innovision.Core.Application.Common;
using Innovision.Core.Application.Common.Models;
using MediatR;

namespace Innovision.Core.Application.Requests.Users.Commands.UpdateAddress;

public class UpdateAddressCommand : IRequest<ApiResponse<bool>>
{
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

    // public AddressCodes AddressCode { get; set; }
}

