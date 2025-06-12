
using Innovision.Core.Common.Models;

public class GetCasesRequest {
    public int OrganizationId { get; set; }
    public  DateTimeOffset StartDate { get; set; }
    public  DateTimeOffset EndDate { get; set; }
    public SupportPagedQuery? PagedQuery { get; set; }
}