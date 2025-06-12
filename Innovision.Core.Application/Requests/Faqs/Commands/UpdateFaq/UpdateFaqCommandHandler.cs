using AutoMapper;
using Innovision.Core.Application.Exceptions;
using Innovision.Core.Application.Interfaces;
using Innovision.Core.Application.Requests.Faqs.Queries;
using Innovision.Core.Domain.Entity;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Innovision.Core.Application.Requests.Faqs.Commads.UpdateFaq;

public class UpdateFaqCommandHandler(ICoreDbContext dbContext, IMapper mapper) : IRequestHandler<UpdateFaqCommand, FaqDto>
{
    private readonly ICoreDbContext _dbContext = dbContext;
    private readonly IMapper _mapper = mapper;

    public async Task<FaqDto> Handle(UpdateFaqCommand request, CancellationToken cancellationToken)
    {
        var faq = await _dbContext.FrequentlyAskQuestions
            .Where(o => o.FrequentlyAskQuestionId == request.FrequentlyAskQuestionId)
            .FirstOrDefaultAsync(cancellationToken);

        _ = faq ?? throw new EntityNotFoundException(typeof(FrequentlyAskQuestion).Name, request.FrequentlyAskQuestionId);

        faq.GameId = request.GameId;
        faq.IsApplicationRelated = request.IsApplicationRelated ? 1 : 0;
        faq.OrderNo = request.OrderNo;
        faq.Question = request.Question;
        faq.Answer = request.Answer;

        _dbContext.FrequentlyAskQuestions.Update(faq);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return _mapper.Map<FaqDto>(faq);
    }
}