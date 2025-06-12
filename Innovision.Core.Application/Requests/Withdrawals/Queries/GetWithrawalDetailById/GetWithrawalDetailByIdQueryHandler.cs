using AutoMapper;
using AutoMapper.QueryableExtensions;
using Innovision.Core.Application.Interfaces;
using Innovision.Core.Application.Requests.Accounts.Queries.GetCurrentAccountInfo;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Innovision.Core.Application.Requests.Withdrawals.Queries.GetWithrawalDetailById;

public class GetWithrawalDetailByIdQueryHandler(IMapper mapper, ICoreDbContext dbContext, IMediator mediator) : IRequestHandler<GetWithrawalDetailByIdQuery, WithdrawalDto>
{
    private readonly IMapper _mapper = mapper;
    private readonly ICoreDbContext _dbContext = dbContext;
    private readonly IMediator _mediator = mediator;

    public async Task<WithdrawalDto> Handle(GetWithrawalDetailByIdQuery request, CancellationToken cancellationToken)
    {
        var account = await _mediator.Send(new GetCurrentAccountInfoQuery(), cancellationToken);

        if (account == null)
            return null;

        var withdrawal = await _dbContext.Withdrawals
            .Where(m => m.TransactionId == request.TransactionId && m.AccountInfoId == account.AccountInfoId)
            .ProjectTo<WithdrawalDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(cancellationToken);

        return withdrawal;
    }
}