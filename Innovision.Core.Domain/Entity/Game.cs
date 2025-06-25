using Innovision.Core.Domain.Common;

namespace Innovision.Core.Domain.Entity;

public partial class Game : AuditableEntity
{
    public int GameId { get; set; }
    public Guid GameObjectId { get; set; } = Guid.NewGuid();
    public string Name { get; set; }
    public string Description { get; set; }
    public int GameCategoryId { get; set; } ////
    public string ExternalGameId { get; set; }
    public int GameProviderId { get; set; }////
    public int GameStatusId { get; set; }////
    public bool IsInternal { get; set; }
    public bool Active { get; set; } = true;
    public int StandardMissedDraws { get; set; }
    public string CoverImage { get; set; }
    public virtual ICollection<Order> Orders { get; set; }
    public virtual ICollection<FrequentlyAskQuestion> FrequentlyAskQuestions { get; set; }
    public virtual ICollection<JackpotWinner> JackpotWinners { get; set; }
    
    public virtual GameCategory GameCategory { get; set; }
    public virtual GameProvider GameProvider { get; set; }
    public virtual GameStatus GameStatus { get; set; }
}
