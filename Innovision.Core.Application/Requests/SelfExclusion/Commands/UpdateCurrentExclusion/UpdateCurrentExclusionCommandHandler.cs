using AutoMapper;
using Innovision.Core.Application.Exceptions;
using Innovision.Core.Application.Interfaces;
using Innovision.Core.Domain.Entity;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Innovision.Core.Application.Requests.SelfExclusion.Commands.UpdateCurrentExclusion;

public class UpdateCurrentExclusionCommandHandler(ICoreDbContext dbContext, IMapper mapper) : IRequestHandler<UpdateCurrentExclusionCommand, SelfExclusionDto>
{
    private readonly ICoreDbContext _dbContext = dbContext;
    private readonly IMapper _mapper = mapper;
    public async Task<SelfExclusionDto> Handle(UpdateCurrentExclusionCommand request, CancellationToken cancellationToken)
    {

        var activeExclusion = await _dbContext.SelfExclusions.Where(m => m.SelfExclusionId == request.SelfExclusionId).FirstOrDefaultAsync(cancellationToken);
        _ = activeExclusion ?? throw new EntityNotFoundException("SelfExclusion", request.SelfExclusionId);

        activeExclusion.DateStart = request.DateStart;
        activeExclusion.DateEnd = request.DateEnd;
        activeExclusion.IsIndefinite = request.IsIndefinite;
        activeExclusion.Status = request.Status;

        _dbContext.SelfExclusions.Update(activeExclusion);

        await _dbContext.SaveChangesAsync(cancellationToken);

        return _mapper.Map<SelfExclusionDto>(activeExclusion);
    }
}
