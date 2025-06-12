using Innovision.Core.Application.Requests.Faqs.Commads.CreateFaq;
using Innovision.Core.Application.Requests.Faqs.Commads.UpdateFaq;
using Innovision.Core.Application.Requests.Faqs.Queries.GetFaqById;
using Innovision.Core.Application.Requests.Faqs.Queries.GetFaqs;
using Microsoft.AspNetCore.Mvc;

namespace Innovision.Core.API.Controllers;

public class FaqController : ApiBaseController
{
    [HttpGet]
    public async Task<ActionResult> GetFaq(CancellationToken cancellationToken)
    {
        var results = await Mediator.Send(new GetFaqsQuery(), cancellationToken);
        return Ok(results);
    }

    [HttpGet("faqId")]
    public async Task<ActionResult> GetFaqById(int faqId, CancellationToken cancellationToken)
    {
        var results = await Mediator.Send(new GetFaqByIdQuery(faqId), cancellationToken);
        return Ok(results);
    }

    [HttpPost("search")]
    public async Task<ActionResult> SearchFaq(SearchFaqsQuery query, CancellationToken cancellationToken)
    {
        var results = await Mediator.Send(query, cancellationToken);
        return Ok(results);
    }

    [HttpPost]
    public async Task<ActionResult> CreateFaq(CreateFaqCommand query, CancellationToken cancellationToken)
    {
        var results = await Mediator.Send(query, cancellationToken);
        return Ok(results);
    }

    [HttpPut]
    public async Task<ActionResult> UpdateFaq(UpdateFaqCommand query, CancellationToken cancellationToken)
    {
        var results = await Mediator.Send(query, cancellationToken);
        return Ok(results);
    }
}