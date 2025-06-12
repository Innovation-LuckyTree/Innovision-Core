namespace Innovision.Core.Infrastructure.GameSchedule.Models.Requests;

public class ClosingDateRequest
{
    public  DateTimeOffset Date { get; set; }
    public bool IsWholeday { get; set; }
    public string CompanyId { get; set; }
    public int ClosedDrawType { get; set; }
}
