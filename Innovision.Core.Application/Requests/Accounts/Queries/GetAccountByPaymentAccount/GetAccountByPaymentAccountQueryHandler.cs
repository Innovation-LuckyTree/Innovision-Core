using AutoMapper;
using AutoMapper.QueryableExtensions;
using Innovision.Core.Application.Interfaces;
using Innovision.Core.Application.Requests.Accounts.Queries.GetCurrentAccountInfo;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Innovision.Core.Application.Requests.Accounts.Queries.GetAccountByPaymentAccount;

public class GetAccountByPaymentAccountQueryHandler(ICoreDbContext coreDbContext, IMapper mapper) : IRequestHandler<GetAccountByPaymentAccountQuery, AccountInfoDto>
{
    private readonly ICoreDbContext _coreDbContext = coreDbContext;
    private readonly IMapper _mapper = mapper;

    public async Task<AccountInfoDto> Handle(GetAccountByPaymentAccountQuery request, CancellationToken cancellationToken)
    {
        var accountInfo = await _coreDbContext.Accounts.Where(o => o.PaymentAccountId == request.PaymentAccountId)
            .ProjectTo<AccountInfoDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(cancellationToken);

        return accountInfo;
    }
}