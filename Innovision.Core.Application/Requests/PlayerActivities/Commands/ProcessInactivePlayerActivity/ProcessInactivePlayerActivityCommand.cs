using MediatR;

namespace Innovision.Core.Application.Requests.PlayerActivities.Commands.ProcessInactivePlayerActivity;

public class ProcessInactivePlayerActivityCommand : IRequest<long>
{
  public Guid CompanyId { get; set; }
  public long AccountInfoId { get; set; }
}
