namespace Innovision.Core.Infrastructure.GameSchedule.Models.Responses;

public class GameScheduleResponse
{
    public int Id { get; set; }
    public int CompanyGame { get; set; }
    public GameDrawTypeResponse GameDrawType { get; set; }
    public  DateTimeOffset Date { get; set; }
    public TimeSpan OpenSchedule { get; set; }
    public int Status { get; set; }
    public bool IsDeleted { get; set; }
}