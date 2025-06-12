using AutoMapper;
using Innovision.Core.Application.Interfaces;
using Innovision.Core.Application.Notifications.AccountNotifications;
using Innovision.Core.Application.Requests.Notifications.Queries;
using Innovision.Core.Domain.Entity;
using MediatR;
namespace Innovision.Core.Application.Requests.Notifications.Commands.CreateNotificationCommand;

public class CreateNotificationCommandHandler(ICoreDbContext coreDbContext, IMapper mapper, IMediator mediator) : IRequestHandler<CreateNotificationCommand, NotificationDto>
{
  private readonly ICoreDbContext _coreDbContext = coreDbContext;
  private readonly IMapper _mapper = mapper;
  private readonly IMediator _mediator = mediator;
  public async Task<NotificationDto> Handle(CreateNotificationCommand request, CancellationToken cancellationToken)
  {
    // store new notification
    Notification notif = new()
    {
      AccountInfoId = request.AccountInfoId,
      NotificationTypeId = request.NotificationTypeId,
      Title = request.Title,
      Description = request.Description,
      RedirectUrl = request.RedirectUrl,
      CreatedOn = DateTime.UtcNow
    };

    _coreDbContext.Notifications.Add(notif);

    await _coreDbContext.SaveChangesAsync(cancellationToken);

    await _mediator.Publish(new BroadcastCountNotification(request.AccountInfoId), cancellationToken).ConfigureAwait(false);

    return _mapper.Map<NotificationDto>(notif);
  }
}