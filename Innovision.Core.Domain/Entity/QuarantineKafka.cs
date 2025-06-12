namespace Innovision.Core.Domain.Entity
{
  public class QuarantineKafka
  {
    public long QuarantineKafkaId { get; set; }
    public string KafkaValue { get; set; }
    public string KafkaTopic { get; set; }
    public int? Attempts { get; set; } = 0;
    public string ErrorCode { get; set; }
    public string ErrorMessage { get; set; }
    public int Status { get; set; } // 1 - active, 2 - completed, 3 - hopeless
    public  DateTimeOffset? CreatedOn { get; set; } = DateTime.UtcNow;
    public  DateTimeOffset? AttemptedOn { get; set; } = null;
    public  DateTimeOffset? CompletedOn { get; set; } = null;
  }
}
