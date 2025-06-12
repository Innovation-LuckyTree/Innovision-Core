using AutoMapper;
using AutoMapper.QueryableExtensions;
using Innovision.Core.Application.Interfaces;
using Innovision.Core.Application.Requests.Accounts.Queries.GetApprovedAccounts;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Innovision.Core.Application.Requests.Branches.Queries.GetAccountsByBranchId;

public class GetAccountsByBranchIdQueryHandler : IRequestHandler<GetAccountsByBranchIdQuery, AccountVm>
{
  private readonly ICoreDbContext _dbContext;
  private readonly IMapper _mapper;

  public GetAccountsByBranchIdQueryHandler(ICoreDbContext dbContext, IMapper mapper)
  {
    _dbContext = dbContext;
    _mapper = mapper;
  }

  public async Task<AccountVm> Handle(GetAccountsByBranchIdQuery request, CancellationToken cancellationToken)
  {
    var accountsQuery = _dbContext.Accounts
        .Where(o => o.BranchId == request.BranchId);

    if (request.UserTypeIds != null && request.UserTypeIds.Any())
    {
      accountsQuery = accountsQuery.Where(o => request.UserTypeIds.Contains(o.UserTypeId));
    }

    var accounts = await accountsQuery
        .ProjectTo<AccountDto>(_mapper.ConfigurationProvider)
        .ToListAsync(cancellationToken);

    return new AccountVm(accounts);
  }
}
