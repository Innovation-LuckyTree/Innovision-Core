using Innovision.Core.Application.Exceptions;
using Innovision.Core.Application.Interfaces;
using Innovision.Core.Domain.Entity;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Innovision.Core.Application.Requests.PlayerActivities.Commands.ProcessInactivePlayerActivity;

public class ProcessInactivePlayerActivityCommandHandler : IRequestHandler<ProcessInactivePlayerActivityCommand, long>
{
  private readonly ICoreDbContext _coreDbContext;

  public ProcessInactivePlayerActivityCommandHandler(ICoreDbContext coreDbContext, IMediator mediator)
  {
    _coreDbContext = coreDbContext;
  }

  public async Task<long> Handle(ProcessInactivePlayerActivityCommand request, CancellationToken cancellationToken)
  {
    var isExist = await _coreDbContext.PlayerActivities
        .Where(o => o.AccountInfoId == request.AccountInfoId)
        .FirstOrDefaultAsync(cancellationToken);

    if (isExist != null)
    {
      return await UpdatePlayerInactivity(request, cancellationToken);
    }

    var playerInactivity = CreatePlayerInactivity(request);

    _coreDbContext.PlayerActivities.Add(playerInactivity);

    await _coreDbContext.SaveChangesAsync(cancellationToken);

    return playerInactivity.ActivityId;
  }

  private async Task<long> UpdatePlayerInactivity(ProcessInactivePlayerActivityCommand request, CancellationToken cancellationToken)
  {
    var playerActivity = await _coreDbContext.PlayerActivities.Where(o => o.AccountInfoId == request.AccountInfoId).FirstOrDefaultAsync(cancellationToken);
    _ = playerActivity ?? throw new EntityNotFoundException(typeof(PlayerActivity).Name, request.AccountInfoId);

    playerActivity.MissedDraws += 1;
    playerActivity.LastModified = DateTime.UtcNow;

    //if (playerActivity.MissedDraws >= (standardMissedDraws + playerActivity.Extended))
    //{
    //  playerActivity.IsActive = false;
    //  playerActivity.RequiredTopay = true;
    //}

    _coreDbContext.PlayerActivities.Update(playerActivity);
    await _coreDbContext.SaveChangesAsync(cancellationToken);

    return playerActivity.ActivityId;
  }

  private PlayerActivity CreatePlayerInactivity(ProcessInactivePlayerActivityCommand request)
      => new()
      {
        AccountInfoId = request.AccountInfoId,
        MissedDraws = 1,
        Extended = 0,
        RequiredTopay = false,
        LastDrawDateTime = null,
        LastDrawTime = null,
        IsActive = true
      };
}
