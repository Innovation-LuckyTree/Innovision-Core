namespace Innovision.Core.Infrastructure.GameSchedule.Models.Requests;

public class GameScheduleRequest
{
    public int CompanyGame { get; set; }
    public  DateTimeOffset Date { get; set; }
    public string GameDrawType { get; set; }
    public int Status { get; set; }
}
