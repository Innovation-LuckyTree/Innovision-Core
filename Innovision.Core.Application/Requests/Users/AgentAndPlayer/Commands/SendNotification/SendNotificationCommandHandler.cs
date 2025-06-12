using Innovision.Core.Application.Common.Contants;
using Innovision.Core.Application.Interfaces;
using Innovision.Core.Application.Requests.Notifications.Commands.CreateAccountsNotificationByName;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Innovision.Core.Application.Requests.Accounts.Users.AgentAndPlayer.Commands.SendNotification;

public class SendNotificationCommandHandler(ICoreDbContext coreDbContext, IMediator mediator) : IRequestHandler<SendNotificationCommand, Unit>
{
    private readonly ICoreDbContext _coreDbContext = coreDbContext;
    private readonly IMediator _mediator = mediator;
    private IEnumerable<int> _verifier = [ 1, 2 ];

    public async Task<Unit> Handle(SendNotificationCommand request, CancellationToken cancellationToken)
    {
        var accounts = await _coreDbContext.Accounts.Include(o => o.Branch)
            .Where(o => _verifier.Contains(o.UserTypeId) &&
            (o.BranchId == -1 || (o.Branch.IsMain)))
            .ToListAsync(cancellationToken);

        var result = await _mediator.Send(new CreateAccountsNotificationByNameCommand(accounts.Select(o => o.AccountInfoId), NotificationTypes.USER_VERIFICATION, NotificationNames.NewVerification), cancellationToken);

        return Unit.Value;
    }
}