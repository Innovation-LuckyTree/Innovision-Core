namespace Innovision.Core.Domain.Entity;

public class GameProvider
{
    public int GameProviderId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string CoverImage { get; set; }
    public virtual ICollection<Game> Game { get; set; }
}