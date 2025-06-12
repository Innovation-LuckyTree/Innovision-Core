namespace Innovision.Core.Infrastructure.GameSchedule.Models.Responses;

public class ClosingDateResponse
{
    public int Id { get; set; }
    public  DateTimeOffset Date { get; set; }
    public bool IsWholeday { get; set; }
    public string CompanyId { get; set; }
    public bool IsDeleted { get; set; }
    public int ClosedDrawType { get; set; }
}