namespace Innovision.Core.Application.Requests.Notifications.Commands.CreateBulkNotification;

public class AccountNotification
{
    public long AccountInfoId { get; set; }
    public int NotificationTypeId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string RedirectUrl { get; set; }
}
