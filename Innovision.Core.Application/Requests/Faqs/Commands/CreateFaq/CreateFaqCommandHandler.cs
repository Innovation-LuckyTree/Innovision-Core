using AutoMapper;
using Innovision.Core.Application.Interfaces;
using Innovision.Core.Application.Requests.Faqs.Queries;
using Innovision.Core.Domain.Entity;
using MediatR;

namespace Innovision.Core.Application.Requests.Faqs.Commads.CreateFaq;

public class CreateFaqCommandHandler(ICoreDbContext dbContext, IMapper mapper) : IRequestHandler<CreateFaqCommand, FaqDto>
{
    private readonly ICoreDbContext _dbContext = dbContext;
    private readonly IMapper _mapper = mapper;

    public async Task<FaqDto> Handle(CreateFaqCommand request, CancellationToken cancellationToken)
    {
        FrequentlyAskQuestion faq = new()
        {
            GameId = request.GameId,
            IsApplicationRelated = request.IsApplicationRelated ? 1 : 0,
            OrderNo = request.OrderNo,
            Question = request.Question,
            Answer = request.Answer
        };

        _dbContext.FrequentlyAskQuestions.Add(faq);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return _mapper.Map<FaqDto>(faq);
    }
}