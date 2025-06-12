namespace Innovision.Core.Application.Requests.Faqs.Queries;

public record FaqVm(IEnumerable<FaqDto> Faqs)
{
    public int Count
    {
        get
        {
            return Faqs?.Count() ?? 0;
        }
    }
}