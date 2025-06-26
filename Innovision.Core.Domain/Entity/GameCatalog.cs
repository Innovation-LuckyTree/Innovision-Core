namespace Innovision.Core.Domain.Entity;

public class GameCatalog
{
    public long GameCatalogId { get; set; }
    public int GameId { get; set; }
    public int GameCategoryId { get; set; }

    public virtual Game Game { get; set; }
    public virtual GameCategory GameCategory { get; set; }
}