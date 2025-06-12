using AutoMapper;
using AutoMapper.QueryableExtensions;
using FluentValidation;
using Innovision.Core.Application.Exceptions;
using Innovision.Core.Application.Interfaces;
using Innovision.Core.Domain.Entity;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Innovision.Core.Application.Requests.Deposits.Queries.LookupReference;

public class LookupReferenceQueryHandler(ICoreDbContext coreDbContext, IMapper mapper) : IRequestHandler<LookupReferenceQuery, DepositDto>
{
    private readonly ICoreDbContext _coreDbContext = coreDbContext;
    private readonly IMapper _mapper = mapper;

    public async Task<DepositDto> Handle(LookupReferenceQuery request, CancellationToken cancellationToken)
    {
        var result = await _coreDbContext.Deposits
            .Include(o => o.AccountInfo)
            .Include(o => o.PaymentMethod)
            .Where(o => o.TransactionNo.Contains(request.TransactionNo) && o.DepositStatusId == 1)
            .ProjectTo<DepositDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(cancellationToken);

        _ = result ?? throw new EntityNotFoundException(typeof(Deposit).Name, request.TransactionNo);
        return result;
    }
}

