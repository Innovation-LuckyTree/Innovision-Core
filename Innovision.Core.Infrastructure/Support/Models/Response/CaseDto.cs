
public class CaseDto
{
    public long CaseId { get; set; }
    public string Fullname { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public long CaseOwnerId { get; set; }
    public int CategoryId { get; set; }
    public int StatusId { get; set; }
    public string Remarks { get; set; }
    public long? ReportedPersonId { get; set; }
    public string Category { get; set; }
    public string Status { get; set; }
    public  DateTimeOffset TicketDate { get; set; }
    public  DateTimeOffset CreatedOn { get; set; }
    public  DateTimeOffset? LastModifiedBy { get; set; }
    public OwnerDto CaseOwner { get; set; }
    public List<CommentDto> Comments { get; set; }
    public string NoOfHours { get; set; }
}