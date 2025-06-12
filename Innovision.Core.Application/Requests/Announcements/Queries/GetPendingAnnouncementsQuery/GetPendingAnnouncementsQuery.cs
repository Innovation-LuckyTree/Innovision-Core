using MediatR;

namespace Innovision.Core.Application.Requests.Announcements.Queries.GetPendingAnnouncementsQuery;

public record GetPendingAnnouncementsQuery : IRequest<AnnouncementVm>
{
  public int BranchId { get; set; }
}
