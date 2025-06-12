using Innovision.Core.Domain.Common;

namespace Innovision.Core.Domain.Entity;

public partial class Game : AuditableEntity
{
    public int GameId { get; set; }
    public Guid GameObjectId { get; set; } = Guid.NewGuid();
    public string Name { get; set; }
    public string Description { get; set; }
    public bool Active { get; set; } = true;
    public int StandardMissedDraws { get; set; }


    public virtual ICollection<GameType> GameTypes { get; set; }
    public virtual ICollection<Order> Orders { get; set; }
    public virtual ICollection<FrequentlyAskQuestion> FrequentlyAskQuestions { get; set; }
    public virtual ICollection<JackpotWinner> JackpotWinners { get; set; }
}
