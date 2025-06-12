using Innovision.Core.Application.Interfaces;
using Innovision.Core.Application.Requests.PlayerActivities.Commands.UpdatePlayerActivity;
using Innovision.Core.Domain.Entity;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Innovision.Core.Application.Requests.PlayerActivities.Commands.CreatePlayerActivity;

public class CreatePlayerActivityCommandHandler : IRequestHandler<CreatePlayerActivityCommand, long>
{
  private readonly ICoreDbContext _coreDbContext;
  private readonly IMediator _mediator;

  public CreatePlayerActivityCommandHandler(ICoreDbContext coreDbContext, IMediator mediator)
  {
    _coreDbContext = coreDbContext;
    _mediator = mediator;
  }

  public async Task<long> Handle(CreatePlayerActivityCommand request, CancellationToken cancellationToken)
  {
    var lastDrawDateTime = request.LastDrawDateTime.HasValue && request.LastDrawTime.HasValue
        ? request.LastDrawDateTime.Value.Date + request.LastDrawTime.Value
        : (DateTime?)null;

    var isExist = await _coreDbContext.PlayerActivities
        .Where(o => o.AccountInfoId == request.AccountInfoId)
        .FirstOrDefaultAsync(cancellationToken);

    if (isExist != null)
    {
      return await UpdatePlayerActivity(request, lastDrawDateTime, cancellationToken);
    }

    var playerActivity = CreatePlayerActivity(request, lastDrawDateTime);

    _coreDbContext.PlayerActivities.Add(playerActivity);

    await _coreDbContext.SaveChangesAsync(cancellationToken);

    return playerActivity.ActivityId;
  }

  private async Task<long> UpdatePlayerActivity(CreatePlayerActivityCommand request, DateTime? lastDrawDateTime, CancellationToken cancellationToken)
  {
    var command = new UpdatePlayerActivityCommand
    {
      AccountInfoId = request.AccountInfoId,
      MissedDraws = 0,
      RequiredTopay = false,
      LastDrawDateTime = lastDrawDateTime,
      LastDrawTime = request.LastDrawTime,
      IsActive = true,
      LastModified = DateTime.UtcNow
    };

    return await _mediator.Send(command, cancellationToken);
  }

  private PlayerActivity CreatePlayerActivity(CreatePlayerActivityCommand request, DateTime? lastDrawDateTime)
      => new()
      {
        AccountInfoId = request.AccountInfoId,
        MissedDraws = 0,
        Extended = 0,
        RequiredTopay = false,
        LastDrawDateTime = lastDrawDateTime,
        LastDrawTime = request.LastDrawTime,
        IsActive = true
      };
}
