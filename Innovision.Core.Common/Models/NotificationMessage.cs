namespace Innovision.Core.Common.Models;

public class NotificationMessage
{
    public string Type { get; set; }
    public string Name { get; set; }
    public string Title { get; set; }
    public string Notifications { get; set; }
    public string? Parameters { get; set; }
    public string Url { get; set; }
}