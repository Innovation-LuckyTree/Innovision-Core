namespace Innovision.Core.Domain.Entity;

public class GameCategory
{
    public int GameCategoryId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string CoverImage { get; set; }

    public virtual ICollection<GameCatalog> GameCatalogs { get; set; }
    public virtual ICollection<GameProvider> GameProviders { get; set; }
    public virtual ICollection<Game> Games { get; set; }
}