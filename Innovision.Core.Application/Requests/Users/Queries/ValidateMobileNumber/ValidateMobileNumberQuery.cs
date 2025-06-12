using Innovision.Core.Application.Common;
using MediatR;

namespace Innovision.Core.Application.Requests.Users.Queries.ValidateMobileNumber;

public record ValidateMobileNumberQuery(string MobileNumber) : IRequest<ApiResponse<bool>>;
