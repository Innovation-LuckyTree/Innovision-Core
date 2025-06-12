using Innovision.Core.Application.Requests.Announcements.Queries;
using MediatR;

namespace Innovision.Core.Application.Requests.Announcements.Commands.CreateAnnouncementCommand;

public class CreateAnnouncementCommand : IRequest<AnnouncementDto>
{
    public int? BranchId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public List<int> SendTo { get; set; }
    public  DateTimeOffset? StartDate { get; set; }
    public  DateTimeOffset? EndDate { get; set; }
    public bool IsBanner { get; set; } = false;
}
