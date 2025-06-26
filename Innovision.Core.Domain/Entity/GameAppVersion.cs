using Innovision.Core.Domain.Common;

namespace Innovision.Core.Domain.Entity;

public partial class GameAppVersion : AuditableEntity
{
    public long GameAppVersionId { get; set; }
    public int GameId { get; set; }
    public string Version { get; set; }
    public bool ForceRefresh { get; set; }
    public int Status { get; set; }
    public string ReleaseNotes { get; set; } = "";

    public virtual Game Game { get; set; }
    public virtual GameAppVersionStatus GameAppVersionStatus { get; set; }
}