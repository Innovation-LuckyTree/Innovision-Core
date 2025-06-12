using AutoMapper;
using Innovision.Core.Application.Interfaces;
using Innovision.Core.Domain.Entity;
using MediatR;

namespace Innovision.Core.Application.Requests.SelfExclusion.Commands.CreateNewExclusion;

public class CreateNewExclusionCommandHandler(ICoreDbContext dbContext, IMapper mapper) : IRequestHandler<CreateNewExclusionCommand, SelfExclusionDto>
{
    private readonly ICoreDbContext _dbContext = dbContext;
    private readonly IMapper _mapper = mapper;
    public async Task<SelfExclusionDto> Handle(CreateNewExclusionCommand request, CancellationToken cancellationToken)
    {

        Domain.Entity.SelfExclusion selfExclusion = new()
        {
            AccountId = request.AccountId,
            IsIndefinite = request.IsIndefinite,
            DateStart = request.DateStart,
            DateEnd = request.DateEnd,
            Status = 1
        };

        _dbContext.SelfExclusions.Add(selfExclusion);

        await _dbContext.SaveChangesAsync(cancellationToken);

        return _mapper.Map<SelfExclusionDto>(selfExclusion);
    }
}
