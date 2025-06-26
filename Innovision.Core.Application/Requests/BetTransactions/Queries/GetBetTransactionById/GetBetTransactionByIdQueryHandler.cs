using AutoMapper;
using AutoMapper.QueryableExtensions;
using Innovision.Core.Application.Interfaces;
using Innovision.Core.Application.Requests.Accounts.Queries.GetCurrentAccountInfo;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Innovision.Core.Application.Requests.BetTransactions.Queries.GetBetTransactionById;

public class GetBetTransactionByIdQueryHandler(ICoreDbContext coreDbContext, IMapper mapper, IMediator mediator) : IRequestHandler<GetBetTransactionByIdQuery, BetTransactionDto>
{
    private readonly ICoreDbContext _coreDbContext = coreDbContext;
    private readonly IMapper _mapper = mapper;
    private readonly IMediator _mediator = mediator;

    public async Task<BetTransactionDto> Handle(GetBetTransactionByIdQuery request, CancellationToken cancellationToken)
    {
        var account = await _mediator.Send(new GetCurrentAccountInfoQuery(), cancellationToken);

        var transaction = await _coreDbContext.BetTransactions
            .Where(x => x.BetTransactionId == request.BetTransactionId)
            .ProjectTo<BetTransactionDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(cancellationToken);

        if (account != null && account.AccountInfoId != (transaction?.BetTransactionId ?? 0))
            return new();

        return transaction;
    }
}
