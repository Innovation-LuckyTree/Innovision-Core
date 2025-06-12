using AutoMapper;
using Innovision.Core.Application.Interfaces;
using Innovision.Core.Application.Notifications.AccountNotifications;
using Innovision.Core.Application.Requests.Notifications.Queries;
using Innovision.Core.Domain.Entity;
using MediatR;
namespace Innovision.Core.Application.Requests.Notifications.Commands.CreateAccountsNotificationCommand;

public class CreateAccountsNotificationCommandHandler(ICoreDbContext coreDbContext, IMapper mapper, IMediator mediator) : IRequestHandler<CreateAccountsNotificationCommand, IEnumerable<NotificationDto>>
{
  private readonly ICoreDbContext _coreDbContext = coreDbContext;
  private readonly IMapper _mapper = mapper;
  private readonly IMediator _mediator = mediator;
  public async Task<IEnumerable<NotificationDto>> Handle(CreateAccountsNotificationCommand request, CancellationToken cancellationToken)
  {
    var notificationList = request.Accounts.Select(o => new Notification
    {
      AccountInfoId = o,
      NotificationTypeId = request.NotificationTypeId,
      Title = request.Title,
      Description = request.Description,
      RedirectUrl = request.RedirectUrl,
      CreatedOn = DateTime.UtcNow
    });

    _coreDbContext.Notifications.AddRange(notificationList);

    await _coreDbContext.SaveChangesAsync(cancellationToken);

    foreach (var notification in notificationList)
    {
      await _mediator.Publish(new BroadcastCountNotification(notification.AccountInfoId), cancellationToken).ConfigureAwait(false);
    }

    return _mapper.Map<IEnumerable<NotificationDto>>(notificationList);
  }
}