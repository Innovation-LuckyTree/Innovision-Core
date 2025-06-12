using AutoMapper;
using Innovision.Core.Application.Interfaces;
using Innovision.Core.Application.Notifications.AccountNotifications;
using Innovision.Core.Application.Requests.Notifications.Queries;
using Innovision.Core.Common.Interfaces;
using Innovision.Core.Domain.Entity;
using MediatR;
namespace Innovision.Core.Application.Requests.Notifications.Commands.CreateBulkNotificationByName;

public class CreateBulkNotificationByNameCommandHandler(ICoreDbContext coreDbContext, IMapper mapper, IMediator mediator, INotificationMessageVm notificationMessage) : IRequestHandler<CreateBulkNotificationByNameCommand, IEnumerable<NotificationDto>>
{
  private readonly ICoreDbContext _coreDbContext = coreDbContext;
  private readonly IMapper _mapper = mapper;
  private readonly IMediator _mediator = mediator;
  private readonly INotificationMessageVm _notificationMessage = notificationMessage;
  public async Task<IEnumerable<NotificationDto>> Handle(CreateBulkNotificationByNameCommand request, CancellationToken cancellationToken)
  {
    var notificationList = new List<Notification>();

    foreach (var requestNotification in request.AccountNotifications)
    {
      var notification = _notificationMessage.GetNotificationMessageByName(requestNotification.Name);
      if (notification == null)
        continue;

      notificationList.Add(new Notification
      {
        AccountInfoId = requestNotification.AccountInfoId,
        NotificationTypeId = requestNotification.NotificationTypeId,
        Title = notification.Title,
        Description = notification.Notifications,
        RedirectUrl = notification.Url,
        CreatedOn = DateTime.UtcNow
      });
    }

    if ((notificationList?.Count ?? 0) == 0)
    {
      return [];
    }

    _coreDbContext.Notifications.AddRange(notificationList);

    await _coreDbContext.SaveChangesAsync(cancellationToken);

    foreach (var notification in notificationList)
    {
      await _mediator.Publish(new BroadcastCountNotification(notification.AccountInfoId), cancellationToken).ConfigureAwait(false);
    }

    return _mapper.Map<IEnumerable<NotificationDto>>(notificationList);
  }
}