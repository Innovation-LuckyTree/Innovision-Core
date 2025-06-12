namespace Innovision.Core.Application.Requests.Notifications.Queries.GetCredtiNotifcationById
{
    public class NotificationRecipient
    {
        public NotificationInfo SenderNotification { get; set; }
        public NotificationInfo RecieverNotification { get; set; }
    }

    public class NotificationInfo
    {
        public string Type { get; set; }
        public string NotificationName { get; set; }
        public List<NotificationAccount> Accounts { get; set; }
        public List<string> Args { get; set; }
    }

    public class NotificationAccount
    {
        public string Name { get; set; }
        public Guid UserId { get; set; }
        public long AccountId { get; set; }
        public int UserTypeId { get; set; }
    }
}
