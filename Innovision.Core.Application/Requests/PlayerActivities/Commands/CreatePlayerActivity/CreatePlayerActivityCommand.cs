using MediatR;

namespace Innovision.Core.Application.Requests.PlayerActivities.Commands.CreatePlayerActivity;

public class CreatePlayerActivityCommand : IRequest<long>
{
  public long AccountInfoId { get; set; }
  public  DateTimeOffset? LastDrawDateTime { get; set; }
  public TimeSpan? LastDrawTime { get; set; }
}
