using Innovision.Core.Domain.Common;

namespace Innovision.Core.Domain.Entity;

public class GameType
{
    public int GameTypeId { get; set; }
    public string GameTypeName { get; set; }
    public string GameTypeDescription { get; set; }
    public string CoverImage { get; set; }
    public virtual ICollection<Game> Game { get; set; }
    public virtual ICollection<OrderItem> OrderItems { get; set; }
    public virtual ICollection<JackpotWinner> JackpotWinners { get; set; }
}