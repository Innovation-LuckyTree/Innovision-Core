using Innovision.Core.Application.Common.Contants;
using Innovision.Core.Application.Interfaces;
using Innovision.Core.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Innovision.Core.Application.Requests.Players.Queries.GetCurrentPlayerAgentInfo;

public class GetCurrentPlayerAgentInfoQueryHandler(ICurrentUserService currentUserService, ICoreDbContext coreDbContext) : IRequestHandler<GetCurrentPlayerAgentInfoQuery, AccountPaymentVm>
{
    private readonly ICurrentUserService _currentUserService = currentUserService;
    private readonly ICoreDbContext _coreDbContext = coreDbContext;

    public async Task<AccountPaymentVm> Handle(GetCurrentPlayerAgentInfoQuery request, CancellationToken cancellationToken)
    {
        var player = await _coreDbContext.Accounts
            .Include(o => o.Branch)
            .Where(m => m.UserId == _currentUserService.UserObjId && m.UserTypeId == UserContants.USER_TYPE_PLAYER)
            .Select(o => new { o.AccountInfoId, o.AccountObjectId, o.FirstName, o.LastName, o.Branch.BranchName, o.RefferralCode })
            .FirstOrDefaultAsync(cancellationToken);

        var agent = await _coreDbContext.Accounts
                .Where(o => o.RefferralKey == player.RefferralCode)
                .Select(o => new { o.AccountInfoId, o.AccountObjectId, o.FirstName, o.LastName, o.RefferralKey })
                .FirstOrDefaultAsync(cancellationToken);

        var accountPaymentVm = new AccountPaymentVm
        {
            Player = new AccountPaymentDto
            {
                AccountId = player.AccountInfoId,
                AccountName = $"{player.FirstName} {player.LastName}",
                AccountObjId = player.AccountObjectId,
                AccountType = "Player"
            },
            Agent = new AccountPaymentDto
            {
                AccountId = agent.AccountInfoId,
                AccountName = $"{agent?.FirstName} {agent?.LastName}",
                AccountObjId = agent.AccountObjectId,
                AccountType = "Agent",
                ReferralKey = agent.RefferralKey
            },
            BranchName = player.BranchName,
        };

        return accountPaymentVm;
    }
}