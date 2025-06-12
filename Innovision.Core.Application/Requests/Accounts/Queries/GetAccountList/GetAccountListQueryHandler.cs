using AutoMapper;
using AutoMapper.QueryableExtensions;
using Innovision.Core.Application.Interfaces;
using Innovision.Core.Application.Requests.Accounts.Queries.GetCurrentAccountInfo;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Innovision.Core.Application.Requests.Accounts.Queries.GetAccountList;

public class GetAccountListQueryHandler : IRequestHandler<GetAccountListQuery, AccountInfoVm>
{
    private readonly ICoreDbContext _coreDbContext;
    private readonly IMapper _mapper;

    public GetAccountListQueryHandler(ICoreDbContext coreDbContext, IMapper mapper)
    {
        _coreDbContext = coreDbContext;
        _mapper = mapper;
    }

    public async Task<AccountInfoVm> Handle(GetAccountListQuery request, CancellationToken cancellationToken)
    {
        var accounts = await _coreDbContext.Accounts.Where(o => request.AccountIds.Contains(o.AccountInfoId))
            .ProjectTo<AccountInfoDto>(_mapper.ConfigurationProvider)
            .ToListAsync();

        return new AccountInfoVm(accounts);
    }
}