using AutoMapper;
using Innovision.Core.Application.Interfaces;
using Innovision.Core.Application.Notifications.AccountNotifications;
using Innovision.Core.Application.Requests.Notifications.Queries;
using Innovision.Core.Common.Interfaces;
using Innovision.Core.Domain.Entity;
using MediatR;
namespace Innovision.Core.Application.Requests.Notifications.Commands.CreateAccountsNotificationByName;

public class CreateAccountsNotificationByNameCommandHandler(ICoreDbContext coreDbContext, IMapper mapper, IMediator mediator, INotificationMessageVm notificationMessage) : IRequestHandler<CreateAccountsNotificationByNameCommand, IEnumerable<NotificationDto>>
{
  private readonly ICoreDbContext _coreDbContext = coreDbContext;
  private readonly IMapper _mapper = mapper;
  private readonly IMediator _mediator = mediator;
  private readonly INotificationMessageVm _notificationMessage = notificationMessage;

  public async Task<IEnumerable<NotificationDto>> Handle(CreateAccountsNotificationByNameCommand request, CancellationToken cancellationToken)
  {
    var notificationMessage = _notificationMessage.GetNotificationMessageByName(request.Name);

    if (notificationMessage == null)
    {
      return [];
    }

    var message = notificationMessage.Notifications;

    if (request.Parameters?.Count() > 0)
    {
      message = string.Format(message, request.Parameters.ToArray());
    }

    var notificationList = request.Accounts.Select(o => new Notification
    {
      AccountInfoId = o,
      NotificationTypeId = request.NotificationTypeId,
      Title = notificationMessage.Title,
      Description = message,
      RedirectUrl = notificationMessage.Url,
      CreatedOn = DateTime.UtcNow
    });

    _coreDbContext.Notifications.AddRange(notificationList);

    await _coreDbContext.SaveChangesAsync(cancellationToken);

    foreach (var notification in notificationList)
    {
      await _mediator.Publish(new BroadcastCountNotification(notification.AccountInfoId), cancellationToken);
    }

    return _mapper.Map<IEnumerable<NotificationDto>>(notificationList);
  }
}