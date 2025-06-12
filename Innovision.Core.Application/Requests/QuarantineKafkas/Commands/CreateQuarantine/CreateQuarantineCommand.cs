using Innovision.Core.Application.Requests.QuarantineKafkas.Queries;
using MediatR;

namespace Innovision.Core.Application.Requests.QuarantineKafkas.Commands.CreateQuarantine;

public class CreateQuarantineCommand : IRequest<QuarantineKafkaDto>
{
  public string KafkaValue { get; set; }
  public string KafkaTopic { get; set; }
  public string ErrorCode { get; set; }
  public string ErrorMessage { get; set; }
}
