using Innovision.Core.Application.Requests.Announcements.Queries;
using MediatR;

namespace Innovision.Core.Application.Requests.Announcements.Commands.UpdateAnnouncementCommand;

public class UpdateAnnouncementCommand : IRequest<AnnouncementDto>
{
  public long AnnouncementId { get; set; }
  public int Status { get; set; }
}
