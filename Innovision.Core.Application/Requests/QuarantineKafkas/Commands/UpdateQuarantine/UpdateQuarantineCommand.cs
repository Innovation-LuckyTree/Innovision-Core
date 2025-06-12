using Innovision.Core.Application.Requests.QuarantineKafkas.Queries;
using MediatR;

namespace Innovision.Core.Application.Requests.QuarantineKafkas.Commands.UpdateQuarantine;

public class UpdateQuarantineCommand : IRequest<QuarantineKafkaDto>
{
  public long QuarantineKafkaId { get; set; }
  public string? KafkaValue { get; set; }
  public int? Attempts { get; set; }
  public string? ErrorCode { get; set; }
  public string? ErrorMessage { get; set; }
  public int? Status { get; set; }
  public  DateTimeOffset? AttemptedOn { get; set; }
  public  DateTimeOffset? CompletedOn { get; set; }
}
