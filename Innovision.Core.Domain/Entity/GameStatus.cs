namespace Innovision.Core.Domain.Entity;

public class GameStatus
{
    public int GameStatusId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public virtual ICollection<Game> Game { get; set; }
}