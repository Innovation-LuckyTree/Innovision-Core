using Innovision.Core.Application.Requests.Users.Commands.CreateAccountHistory;
using MediatR;

namespace Innovision.Core.Application.Notifications.AccountHistoryNotification;

public record AccountHistoryNotification(long AccountInfoId, string Action) : INotification;

public class AccountHistoryNotificationHandler : INotificationHandler<AccountHistoryNotification>
{
    private readonly IMediator _mediator;

    public AccountHistoryNotificationHandler(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task Handle(AccountHistoryNotification notification, CancellationToken cancellationToken)
    {
        var accountHistoryCommand = new CreateAccountHistoryCommand(notification.AccountInfoId, notification.Action);
        await _mediator.Send(accountHistoryCommand, cancellationToken);
    }
}
