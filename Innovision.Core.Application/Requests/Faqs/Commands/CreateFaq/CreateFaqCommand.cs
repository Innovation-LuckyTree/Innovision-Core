using Innovision.Core.Application.Requests.Faqs.Queries;
using MediatR;

namespace Innovision.Core.Application.Requests.Faqs.Commads.CreateFaq;

public class CreateFaqCommand : IRequest<FaqDto>
{
    public int GameId { get; set; }
    public bool IsApplicationRelated { get; set; }
    public int OrderNo { get; set; }
    public string Question { get; set; }
    public string Answer { get; set; }
}
