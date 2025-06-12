namespace Innovision.Core.Infrastructure.WebsocketServices.Models.Requests;

public class BroadcastNotificationCountRequest
{
  public long AccountId { get; set; }
  public int UnreadCount { get; set; }
}
