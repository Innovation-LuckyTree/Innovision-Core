using AutoMapper;
using Innovision.Core.Application.Exceptions;
using Innovision.Core.Application.Interfaces;
using Innovision.Core.Domain.Entity;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Innovision.Core.Application.Requests.ApplicationVersions.Commands.UpdateSelfLimit;

public class UpdateSelfLimitCommandHandler(ICoreDbContext coreDbContext, IMapper mapper) : IRequestHandler<UpdateSelfLimitCommand, SelfLimitDto>
{
    private readonly ICoreDbContext _coreDbContext = coreDbContext;
    private readonly IMapper _mapper = mapper;

    public async Task<SelfLimitDto> Handle(UpdateSelfLimitCommand request, CancellationToken cancellationToken)
    {
        var selfLimit = await _coreDbContext.SelfLimits.Where(o => o.SelfLimitId == request.SelfLimitId)
            .FirstOrDefaultAsync(cancellationToken);

        _ = selfLimit ?? throw new EntityNotFoundException(typeof(SelfLimit).Name, request.SelfLimitId);

        selfLimit.AmountLimit = request.AmountLimit;
        selfLimit.Status = request.Status;

        _coreDbContext.SelfLimits.Update(selfLimit);

        await _coreDbContext.SaveChangesAsync(cancellationToken);

        return _mapper.Map<SelfLimitDto>(selfLimit);
    }
}