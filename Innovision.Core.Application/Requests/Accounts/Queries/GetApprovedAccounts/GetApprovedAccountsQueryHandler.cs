using AutoMapper;
using AutoMapper.QueryableExtensions;
using Innovision.Core.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Innovision.Core.Application.Requests.Accounts.Queries.GetApprovedAccounts;

public class GetApprovedAccountsQueryHandler : IRequestHandler<GetApprovedAccountsQuery, AccountVm>
{
    private readonly ICoreDbContext _dbContext;
    private readonly IMapper _mapper;

    public GetApprovedAccountsQueryHandler(ICoreDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<AccountVm> Handle(GetApprovedAccountsQuery request, CancellationToken cancellationToken)
    {
        var accounts = await _dbContext.Accounts
            .Where(o => o.IsActive && o.AccountStatusId == Domain.Enums.AccountStatus.Approved)
            .ProjectTo<AccountDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        return new AccountVm(accounts);
    }
}