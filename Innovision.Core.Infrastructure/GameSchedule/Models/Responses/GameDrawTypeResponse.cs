namespace Innovision.Core.Infrastructure.GameSchedule.Models.Responses;

public class GameDrawTypeResponse
{
  public int Id { get; set; }
  public string CompanyId { get; set; }
  public string GameDrawTypeName { get; set; }
  public TimeSpan OpenSchedule { get; set; }
  public TimeSpan EndCutOff { get; set; }
  public TimeSpan DrawTime { get; set; }
  public bool IsDeleted { get; set; }
}