namespace Innovision.Core.Domain.Entity;

public class GameProvider
{
    public int GameProviderId { get; set; }
    public Guid GameProviderUuid { get; set; } = Guid.NewGuid();
    public string Name { get; set; }
    public string Description { get; set; }
    public string CoverImage { get; set; }
    public bool IsExternal { get; set; }
    public bool IsActive { get; set; } = true;
    public string Configuration { get; set; }

    public virtual ICollection<Game> Games { get; set; }
}