using Innovision.Core.Application.Exceptions;
using Innovision.Core.Application.Interfaces;
using Innovision.Core.Domain.Entity;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Innovision.Core.Application.Requests.PlayerActivities.Commands.UpdatePlayerActivity;

public class UpdatePlayerActivityCommandHandler(ICoreDbContext coreDbContext) : IRequestHandler<UpdatePlayerActivityCommand, long>
{
  private readonly ICoreDbContext _coreDbContext = coreDbContext;

  public async Task<long> Handle(UpdatePlayerActivityCommand request, CancellationToken cancellationToken)
  {
    var playerActivity = await _coreDbContext.PlayerActivities.Where(o => o.AccountInfoId == request.AccountInfoId).FirstOrDefaultAsync(cancellationToken);

    _ = playerActivity ?? throw new EntityNotFoundException(typeof(PlayerActivity).Name, request.AccountInfoId);

    playerActivity.MissedDraws = request.MissedDraws;
    playerActivity.RequiredTopay = request.RequiredTopay;
    playerActivity.LastDrawDateTime = request.LastDrawDateTime;
    playerActivity.LastDrawTime = request.LastDrawTime;
    playerActivity.IsActive = request.IsActive;
    playerActivity.LastModified = request.LastModified;

    _coreDbContext.PlayerActivities.Update(playerActivity);

    await _coreDbContext.SaveChangesAsync(cancellationToken);

    return playerActivity.ActivityId;
  }
}