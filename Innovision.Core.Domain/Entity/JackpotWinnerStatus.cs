namespace Innovision.Core.Domain.Entity;

public class JackpotWinnerStatus
{
    public int JackpotWinnerStatusId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }

    public virtual ICollection<JackpotWinner> JackpotWinners { get; set; }
}
