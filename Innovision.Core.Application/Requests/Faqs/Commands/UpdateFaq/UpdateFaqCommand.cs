using Innovision.Core.Application.Requests.Faqs.Queries;
using MediatR;

namespace Innovision.Core.Application.Requests.Faqs.Commads.UpdateFaq;

public class UpdateFaqCommand : IRequest<FaqDto>
{
    public int FrequentlyAskQuestionId { get; set; }
    public int GameId { get; set; }
    public bool IsApplicationRelated { get; set; }
    public int OrderNo { get; set; }
    public string Question { get; set; }
    public string Answer { get; set; }
}
