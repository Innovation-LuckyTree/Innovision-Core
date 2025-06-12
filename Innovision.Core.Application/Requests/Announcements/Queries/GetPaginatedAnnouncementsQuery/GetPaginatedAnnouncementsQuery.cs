using Innovision.Core.Application.Common;
using Innovision.Core.Common.Models;
using MediatR;

namespace Innovision.Core.Application.Requests.Announcements.Queries.GetPaginatedAnnouncementsQuery;

public class GetPaginatedAnnouncementsQuery : IRequest<ApiResponse<AnnouncementVm>>
{
  public int? CompanyId { get; set; }
  public int? BranchId { get; set; }
  public List<int>? SendTo { get; set; } = null;
  public  DateTimeOffset? StartDate { get; set; } = DateTime.UtcNow.AddDays(-30); // default to 1 month data
  public  DateTimeOffset? EndDate { get; set; } = DateTime.UtcNow;
  public PagedQuery? PagedQuery { get; set; }
}
