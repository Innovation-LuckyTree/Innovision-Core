namespace Innovision.Core.Application.Requests.Notifications.Queries.GetNotificationsByAccountId;

public record NotificationVm(IEnumerable<NotificationDto> Notifications)
{
    public int TotalUnreadCount { get; set; }
    public int TotalReadCount { get; set; }
    public int Total { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
}