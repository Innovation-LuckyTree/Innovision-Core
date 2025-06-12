using AutoMapper;
using AutoMapper.QueryableExtensions;
using Innovision.Core.Application.Exceptions;
using Innovision.Core.Application.Interfaces;
using Innovision.Core.Domain.Entity;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Innovision.Core.Application.Requests.Deposits.Queries.GetDepositById;

public class GetDepositByIdQueryHandler(ICoreDbContext coreDbContext, IMapper mapper) : IRequestHandler<GetDepositByIdQuery, DepositDto>
{
    private readonly ICoreDbContext _coreDbContext = coreDbContext;
    private readonly IMapper _mapper = mapper;

    public async Task<DepositDto> Handle(GetDepositByIdQuery request, CancellationToken cancellationToken)
    {
        var result = await _coreDbContext.Deposits
            .Include(o => o.AccountInfo)
            .Include(o => o.PaymentMethod)
            .Where(o => o.DepositId == request.DepositId)
            .ProjectTo<DepositDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(cancellationToken);

        _ = result ?? throw new EntityNotFoundException(typeof(Deposit).Name, request.DepositId);
        return result;
    }
}
