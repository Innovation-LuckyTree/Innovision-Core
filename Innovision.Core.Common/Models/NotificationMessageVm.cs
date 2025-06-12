using Innovision.Core.Common.Interfaces;

namespace Innovision.Core.Common.Models;

public record NotificationMessageVm(IEnumerable<NotificationMessage> NotificationMessages) : INotificationMessageVm
{
    public NotificationMessage GetNotificationMessageByName(string name)
    {
        return NotificationMessages.Where(o => o.Name == name).FirstOrDefault();
    }
}
