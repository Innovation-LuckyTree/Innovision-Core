namespace Innovision.Core.Domain.Entity;

public partial class GameAppVersionStatus
{
    public int StatusId { get; set; }
    public string Name { get; set; }

    public virtual ICollection<GameAppVersion> GameAppVersions { get; set; }
}