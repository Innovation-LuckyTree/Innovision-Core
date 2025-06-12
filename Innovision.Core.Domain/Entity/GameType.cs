using Innovision.Core.Domain.Common;

namespace Innovision.Core.Domain.Entity;

public partial class GameType : AuditableEntity
{
    public int GameTypeId { get; set; }
    public int GameId { get; set; }
    public int GameReferenceId { get; set; } // from game api
    public string GameTypeName { get; set; }
    public string GameTypeDesciption { get; set; }
    public decimal CardPrice { get; set; }

    public virtual Game Game { get; set; }
    public virtual ICollection<OrderItem> OrderItems { get; set; }
    public virtual ICollection<JackpotWinner> JackpotWinners { get; set; }
}
