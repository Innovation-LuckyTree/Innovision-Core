using Innovision.Core.Application.Common;
using MediatR;

namespace Innovision.Core.Application.Requests.Users.Commands.UpdatePersonalDetails;

public class UpdatePersonalDetailsCommand : IRequest<ApiResponse<bool>>
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string MiddleName { get; set; }
    public string Gender { get; set; }
    public string MartialStatus { get; set; }
    public string Nationality { get; set; }
    public string BirthDate { get; set; }
    public string PlaceOfBirth { get; set; }
}
