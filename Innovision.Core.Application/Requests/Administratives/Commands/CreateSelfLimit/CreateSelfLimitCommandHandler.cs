using AutoMapper;
using Innovision.Core.Application.Exceptions;
using Innovision.Core.Application.Interfaces;
using Innovision.Core.Domain.Entity;
using MediatR;

namespace Innovision.Core.Application.Requests.ApplicationVersions.Commands.CreateSelfLimit;

public class CreateSelfLimitCommandHandler(ICoreDbContext coreDbContext, IMapper mapper) : IRequestHandler<CreateSelfLimitCommand, SelfLimitDto>
{
    private readonly ICoreDbContext _coreDbContext = coreDbContext;
    private readonly IMapper _mapper = mapper;

    public async Task<SelfLimitDto> Handle(CreateSelfLimitCommand request, CancellationToken cancellationToken)
    {
        if ((request?.AccountId ?? 0) == 0)
            throw new EntityNotFoundException(typeof(Account).Name, request.AccountId);

        SelfLimit selfLimit = new()
        {
            AccountId = request.AccountId,
            AmountLimit = request.AmountLimit,
            Status = 1
        };

        _coreDbContext.SelfLimits.Add(selfLimit);
        await _coreDbContext.SaveChangesAsync(cancellationToken);

        return _mapper.Map<SelfLimitDto>(selfLimit);
    }
}