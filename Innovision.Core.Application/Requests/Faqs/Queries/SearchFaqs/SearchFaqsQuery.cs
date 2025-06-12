using Innovision.Core.Common.Models;
using MediatR;

namespace Innovision.Core.Application.Requests.Faqs.Queries.GetFaqs;

public class SearchFaqsQuery : IRequest<FaqVm>
{
    public int? GameId { get; set; }
    public bool? IsApplicationRelated { get; set; }
    public PagedQuery? PagedQuery { get; set; }
}
