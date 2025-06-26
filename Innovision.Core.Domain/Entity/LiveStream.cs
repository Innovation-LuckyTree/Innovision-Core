using Innovision.Core.Domain.Common;

namespace Innovision.Core.Domain.Entity;

public class LiveStream : AuditableEntity
{
    public int LiveStreamId { get; set; }
    public string Title { get; set; }
    public string Link { get; set; }
    public string Description { get; set; }
    public int GameId { get; set; }

    public virtual Game Game { get; set; }
}