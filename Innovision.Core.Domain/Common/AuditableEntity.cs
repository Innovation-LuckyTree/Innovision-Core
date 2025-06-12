namespace Innovision.Core.Domain.Common;

public class AuditableEntity
{
    public  DateTimeOffset CreatedOn { get; set; } = DateTime.UtcNow;
    public string CreatedBy { get; set; } = "System";
    public DateTimeOffset? LastModified { get; set; }
    public string ModifiedBy { get; set; } = "System";
}