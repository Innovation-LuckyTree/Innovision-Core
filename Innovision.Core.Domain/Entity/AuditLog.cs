namespace Innovision.Core.Domain.Entity;

public class AuditLog
{
    public long Id { get; set; }
    public string TableName { get; set; }
    public string Action { get; set; } // e.g., "Insert", "Update", "Delete"
    public string KeyValues { get; set; } // Primary key values
    public string OldValues { get; set; } // JSON of previous values
    public string NewValues { get; set; } // JSON of new values
    public string UserId { get; set; } // Optional: track the user making the change
    public  DateTimeOffset Timestamp { get; set; }
}