using Innovision.Core.Application.Common;
using MediatR;

namespace Innovision.Core.Application.Requests.Users.Commands.UpdateProfileImage;

public class UpdateProfileImageCommand : IRequest<ApiResponse<bool>>
{
    public string ProfilePath { get; set; }
}

