using Innovision.Core.Common.Models;

namespace Innovision.Core.Common.Interfaces;

public interface INotificationMessageVm
{
    NotificationMessage GetNotificationMessageByName(string name);
}