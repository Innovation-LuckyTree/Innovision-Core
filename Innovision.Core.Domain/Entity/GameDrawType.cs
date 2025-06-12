namespace Innovision.Core.Domain.Entity;

public partial class GameDrawType
{
    public int GameDrawTypeId { get; set; }
    public int GameTypeId { get; set; }
    public string DrawTypeName { get; set; }
    public TimeSpan DrawSchedule { get; set; }
    public TimeSpan StartCutOff { get; set; }
    public TimeSpan EndCutOff { get; set; }

    public virtual GameType GameType { get; set; }
}