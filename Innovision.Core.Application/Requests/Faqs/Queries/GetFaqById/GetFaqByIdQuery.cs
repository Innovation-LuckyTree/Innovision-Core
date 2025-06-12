using MediatR;

namespace Innovision.Core.Application.Requests.Faqs.Queries.GetFaqById;

public record GetFaqByIdQuery(int FrequentlyAskQuestionId) : IRequest<FaqDto>;
