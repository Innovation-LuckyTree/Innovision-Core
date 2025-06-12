using Innovision.Core.Application.Common;
using MediatR;

namespace Innovision.Core.Application.Requests.Users.Commands.UpdateNotificationSetting;
public class UpdateNotificationSettingCommand : IRequest<ApiResponse<bool>>
{
    public bool InAppNotification { get; set; } = false;
    public bool SmsNotification { get; set; } = false;
    public bool EmailNotification { get; set; } = false;
}
