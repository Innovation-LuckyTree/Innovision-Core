using Innovision.Core.Domain.Common;

namespace Innovision.Core.Domain.Entity;

public partial class JackpotWinnerAttachment : AuditableEntity
{
    public long JackpotWinnerAttachmentId { get; set; }
    public long JackpotWinnerId { get; set; }
    public string FileName { get; set; }
    public string FilePath { get; set; }
    public string FileType { get; set; }

    public virtual JackpotWinner JackpotWinner { get; set; }
}