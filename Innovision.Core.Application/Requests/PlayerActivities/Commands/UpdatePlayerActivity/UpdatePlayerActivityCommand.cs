using MediatR;

namespace Innovision.Core.Application.Requests.PlayerActivities.Commands.UpdatePlayerActivity;

public class UpdatePlayerActivityCommand : IRequest<long>
{
  public long AccountInfoId { get; set; }
  public int MissedDraws { get; set; } = 0;
  public bool RequiredTopay { get; set; } = false;
  public  DateTimeOffset? LastDrawDateTime { get; set; }
  public TimeSpan? LastDrawTime { get; set; }
  public bool IsActive { get; set; } = true;
  public  DateTimeOffset? LastModified { get; set; } = DateTime.UtcNow;
}
