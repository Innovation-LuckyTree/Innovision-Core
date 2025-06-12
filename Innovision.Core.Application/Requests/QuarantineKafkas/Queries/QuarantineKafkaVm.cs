namespace Innovision.Core.Application.Requests.QuarantineKafkas.Queries;

public record QuarantineKafkaVm(IEnumerable<QuarantineKafkaDto> QuarantineKafkas)
{
  public int Count
  {
    get => QuarantineKafkas?.Count() ?? 0;
  }
}