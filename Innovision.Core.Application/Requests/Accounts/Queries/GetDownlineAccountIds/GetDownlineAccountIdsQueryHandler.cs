using AutoMapper;
using AutoMapper.QueryableExtensions;
using Innovision.Core.Application.Exceptions;
using Innovision.Core.Application.Interfaces;
using Innovision.Core.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Innovision.Core.Application.Requests.Accounts.Queries.GetDownlineAccountIds;

public class GetDownlineAccountIdsQueryHandler(ICoreDbContext coreDbContext, IMapper mapper) : IRequestHandler<GetDownlineAccountIdsQuery, DownlineAccountIdDto>
{
    private readonly ICoreDbContext _coreDbContext = coreDbContext;
    private readonly IMapper _mapper = mapper;

    public async Task<DownlineAccountIdDto> Handle(GetDownlineAccountIdsQuery request, CancellationToken cancellationToken)
    {
        IEnumerable<DownlineAccountInfo> downlineAccounts = [];

        var account = await _coreDbContext.Accounts.Where(o => o.AccountInfoId == request.AccountId)
            .FirstOrDefaultAsync(cancellationToken);

        _ = account ?? throw new EntityNotFoundException("AccountInfo", request.AccountId);

        downlineAccounts = await GetDownlineAccountsAsync(downlineAccounts.ToList(), [account.RefferralKey], cancellationToken);

        return new DownlineAccountIdDto(downlineAccounts);
    }

    private async Task<IEnumerable<DownlineAccountInfo>> GetDownlineAccountsAsync(List<DownlineAccountInfo> downlineAccountList, IEnumerable<string> referralKeys, CancellationToken cancellationToken)
    {
        if ((referralKeys?.Count() ?? 0) == 0)
            return downlineAccountList;

        var downlines = await GetDownlineAccountByReferralKeys(referralKeys, cancellationToken);

        if ((downlines?.Count() ?? 0) == 0)
            return downlineAccountList;

        downlineAccountList.AddRange(downlines);

        var nonPlayers = downlines.Where(o => o.UserTypeId != UserTypes.Player).ToList();

        if (nonPlayers.Count == 0)
            return downlineAccountList;

        return await GetDownlineAccountsAsync(downlineAccountList, nonPlayers.Select(o => o.ReferralKey), cancellationToken);
    }

    private async Task<IEnumerable<DownlineAccountInfo>> GetDownlineAccountByReferralKeys(IEnumerable<string> referralKeys, CancellationToken cancellationToken)
    {
        return await _coreDbContext.Accounts.Where(o => referralKeys.Contains(o.RefferralCode))
            .ProjectTo<DownlineAccountInfo>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);
    }
}