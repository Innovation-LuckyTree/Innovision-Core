using Innovision.Core.Application.Interfaces;
using MediatR;
using Innovision.Core.Application.Requests.Notifications.Queries;
using Microsoft.EntityFrameworkCore;
using Innovision.Core.Application.Exceptions;
using Innovision.Core.Infrastructure.WebsocketServices.Models.Requests;
using Innovision.Core.Infrastructure.Interfaces;
using AutoMapper;

namespace Innovision.Core.Application.Requests.Notifications.Commands.UpdateNotificationCommand;

public class UpdateNotificationCommandHandler(ICoreDbContext coreDbContext, IWebsocketServicesApi websocketServicesApi, IMapper mapper) : IRequestHandler<UpdateNotificationCommand, NotificationDto>
{
  private readonly ICoreDbContext _coreDbContext = coreDbContext;
  private readonly IWebsocketServicesApi _websocketServicesApi = websocketServicesApi;
  private readonly IMapper _mapper = mapper;

  public async Task<NotificationDto> Handle(UpdateNotificationCommand request, CancellationToken cancellationToken)
  {
    var notif = await _coreDbContext.Notifications
        .Where(o => o.NotificationId == request.NotificationId)
        .FirstOrDefaultAsync(cancellationToken) ?? throw new EntityNotFoundException("Notification", request.NotificationId);

    notif.IsRead = request.IsRead;
        await _coreDbContext.SaveChangesAsync(cancellationToken);

    // trigger broadcasting of new counter after update changes
    var unreadCount = await _coreDbContext.Notifications
        .Where(n => n.AccountInfoId == request.AccountInfoId && !n.IsRead)
        .CountAsync(cancellationToken);

    var newNotifCountToBroadcast = new BroadcastNotificationCountRequest
    {
      AccountId = request.AccountInfoId,
      UnreadCount = unreadCount
    };

    // trigger websocket broadcast
    var broadcast = await _websocketServicesApi.PostNotificationAsync(newNotifCountToBroadcast, cancellationToken);

    return _mapper.Map<NotificationDto>(notif);
  }
}