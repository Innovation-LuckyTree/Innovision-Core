
public partial class CommentDto
{

    public long CaseCommentId { get; set; }
    public long CaseId { get; set; }
    public string Comment { get; set; }
    public long AccountId { get; set; }
    public int Status { get; set; } = 0;
    public  DateTimeOffset CreatedOn { get; set; }

}